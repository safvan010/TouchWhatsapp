using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WhatsappAutomation.Services
{
    public class UpdateInfo
    {
        public bool HasUpdate { get; set; }
        public string CurrentVersion { get; set; }
        public string LatestVersion { get; set; }
        public string DownloadUrl { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string ReleaseNotes { get; set; }
        public string Error { get; set; }
    }

    public class AppUpdateService
    {
        private const string DefaultUserAgent = "WhatsAppAutomation-AppUpdater";

        public string GetCurrentVersion()
        {
            try
            {
                var ver = Assembly.GetExecutingAssembly().GetName().Version;
                if (ver != null)
                {
                    return $"v{ver.Major}.{ver.Minor}.{ver.Build}";
                }
            }
            catch { }

            try
            {
                string prodVer = Application.ProductVersion;
                if (!string.IsNullOrWhiteSpace(prodVer))
                {
                    return prodVer.StartsWith("v", StringComparison.OrdinalIgnoreCase) ? prodVer : $"v{prodVer}";
                }
            }
            catch { }

            return "v1.0.0";
        }

        public async Task<UpdateInfo> CheckForUpdatesAsync(string updateSource)
        {
            var info = new UpdateInfo
            {
                CurrentVersion = GetCurrentVersion()
            };

            if (string.IsNullOrWhiteSpace(updateSource))
            {
                info.Error = "No update source configured. Please enter your GitHub repo (e.g. owner/repo) or a manifest JSON URL in Settings.";
                return info;
            }

            string cleanSource = updateSource.Trim();

            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                // Case 1: Custom JSON manifest URL
                if (cleanSource.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    cleanSource.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    if (cleanSource.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || cleanSource.Contains("/manifest") || cleanSource.Contains("/version"))
                    {
                        return await CheckViaJsonManifestAsync(cleanSource, info);
                    }
                    else if (cleanSource.Contains("github.com"))
                    {
                        // User pasted full GitHub repository URL e.g. https://github.com/owner/repo
                        var match = Regex.Match(cleanSource, @"github\.com/([^/]+)/([^/]+)");
                        if (match.Success)
                        {
                            cleanSource = $"{match.Groups[1].Value}/{match.Groups[2].Value.Replace(".git", "")}";
                        }
                    }
                }

                // Case 2: GitHub repo (owner/repo)
                if (cleanSource.Contains("/"))
                {
                    return await CheckViaGitHubReleasesAsync(cleanSource, info);
                }

                info.Error = "Invalid update source format. Expected 'owner/repo' or 'https://.../version.json'.";
                return info;
            }
            catch (Exception ex)
            {
                info.Error = $"Check failed: {ex.Message}";
                return info;
            }
        }

        private async Task<UpdateInfo> CheckViaGitHubReleasesAsync(string repoSlug, UpdateInfo info)
        {
            string apiUrl = $"https://api.github.com/repos/{repoSlug}/releases/latest";

            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", DefaultUserAgent);

                string json = await Task.Run(() => client.DownloadString(apiUrl));
                var release = JObject.Parse(json);

                string latestTag = release["tag_name"]?.ToString() ?? "";
                string body = release["body"]?.ToString() ?? "No release notes provided.";
                info.LatestVersion = latestTag.StartsWith("v", StringComparison.OrdinalIgnoreCase) ? latestTag : $"v{latestTag}";
                info.ReleaseNotes = body.Trim();

                var assets = release["assets"] as JArray;
                if (assets != null && assets.Count > 0)
                {
                    // Prioritize .exe or .zip
                    JToken chosenAsset = null;
                    foreach (var asset in assets)
                    {
                        string name = asset["name"]?.ToString() ?? "";
                        if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            chosenAsset = asset;
                            if (name.IndexOf("WhatsappAutomation", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                // Exact name match found, highest priority
                                break;
                            }
                        }
                    }

                    if (chosenAsset == null)
                    {
                        chosenAsset = assets[0];
                    }

                    info.FileName = chosenAsset["name"]?.ToString() ?? "WhatsappAutomation-Update.exe";
                    info.DownloadUrl = chosenAsset["browser_download_url"]?.ToString() ?? "";
                    if (long.TryParse(chosenAsset["size"]?.ToString(), out long size))
                    {
                        info.FileSize = size;
                    }
                }
                else
                {
                    // Fallback to release zipball if no compiled assets are attached
                    info.FileName = $"{repoSlug.Replace("/", "-")}-{latestTag}.zip";
                    info.DownloadUrl = release["zipball_url"]?.ToString();
                }

                if (string.IsNullOrEmpty(info.DownloadUrl))
                {
                    info.Error = $"Latest release {latestTag} has no downloadable files attached.";
                    return info;
                }

                info.HasUpdate = IsNewerVersion(info.LatestVersion, info.CurrentVersion);
                return info;
            }
        }

        private async Task<UpdateInfo> CheckViaJsonManifestAsync(string jsonUrl, UpdateInfo info)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", DefaultUserAgent);

                string json = await Task.Run(() => client.DownloadString(jsonUrl));
                var manifest = JObject.Parse(json);

                string version = manifest["version"]?.ToString() ?? "";
                info.LatestVersion = version.StartsWith("v", StringComparison.OrdinalIgnoreCase) ? version : $"v{version}";
                info.DownloadUrl = manifest["downloadUrl"]?.ToString() ?? "";
                info.FileName = manifest["fileName"]?.ToString() ?? Path.GetFileName(info.DownloadUrl) ?? "WhatsappAutomation-Update.exe";
                info.ReleaseNotes = manifest["releaseNotes"]?.ToString() ?? manifest["changelog"]?.ToString() ?? "Bug fixes and performance improvements.";

                if (string.IsNullOrEmpty(info.DownloadUrl))
                {
                    info.Error = "Manifest JSON missing 'downloadUrl'.";
                    return info;
                }

                info.HasUpdate = IsNewerVersion(info.LatestVersion, info.CurrentVersion);
                return info;
            }
        }

        public async Task<(bool Success, string DownloadedPath, string Error)> DownloadUpdateAsync(
            string downloadUrl, 
            string targetFileName, 
            Action<int, long, long> onProgress = null)
        {
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                string tempDir = Path.Combine(Path.GetTempPath(), "WhatsappAutomation_Update");
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }

                string localFilePath = Path.Combine(tempDir, targetFileName);
                if (File.Exists(localFilePath))
                {
                    try { File.Delete(localFilePath); } catch { }
                }

                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", DefaultUserAgent);

                    if (onProgress != null)
                    {
                        client.DownloadProgressChanged += (s, e) =>
                        {
                            onProgress(e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive);
                        };
                    }

                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), localFilePath);
                }

                var fi = new FileInfo(localFilePath);
                if (!fi.Exists || fi.Length == 0)
                {
                    return (false, null, "Downloaded file is empty.");
                }

                return (true, localFilePath, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public void ApplyUpdateAndRestart(string downloadedFilePath)
        {
            if (string.IsNullOrEmpty(downloadedFilePath) || !File.Exists(downloadedFilePath))
            {
                throw new FileNotFoundException("Downloaded update file not found.", downloadedFilePath);
            }

            string currentExePath = Application.ExecutablePath;
            string appDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/');
            int currentPid = Process.GetCurrentProcess().Id;
            bool isZip = downloadedFilePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase);

            string tempDir = Path.GetDirectoryName(downloadedFilePath);
            string scriptPath = Path.Combine(tempDir, "apply_update.bat");

            // Build atomic batch script that waits for current process to exit, replaces files, and restarts
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("@echo off");
            sb.AppendLine("title Updating WhatsApp Automation...");
            sb.AppendLine("color 0A");
            sb.AppendLine("echo ===================================================");
            sb.AppendLine("echo       UPDATING WHATSAPP AUTOMATION");
            sb.AppendLine("echo ===================================================");
            sb.AppendLine("echo Please wait while files are being updated...");
            sb.AppendLine("echo.");

            // Wait for main application process to close
            sb.AppendLine($"echo Waiting for process {currentPid} to terminate...");
            sb.AppendLine($"taskkill /f /pid {currentPid} >nul 2>&1");
            sb.AppendLine(":WAIT_LOOP");
            sb.AppendLine($"tasklist /fi \"PID eq {currentPid}\" 2>nul | find \"{currentPid}\" >nul");
            sb.AppendLine("if not errorlevel 1 (");
            sb.AppendLine("    timeout /t 1 /nobreak >nul");
            sb.AppendLine("    goto WAIT_LOOP");
            sb.AppendLine(")");
            sb.AppendLine("timeout /t 1 /nobreak >nul");
            sb.AppendLine("echo Process stopped. Installing update...");

            if (isZip)
            {
                // Extract zip contents directly over appDir using built-in PowerShell
                sb.AppendLine("echo Extracting update package...");
                sb.AppendLine($"powershell -NoProfile -ExecutionPolicy Bypass -Command \"Expand-Archive -LiteralPath '{downloadedFilePath}' -DestinationPath '{appDir}' -Force\"");
            }
            else
            {
                // Single exe file update - copy over the main executable
                sb.AppendLine("echo Installing updated executable...");
                sb.AppendLine($"copy /y \"{downloadedFilePath}\" \"{currentExePath}\"");
            }

            sb.AppendLine("echo.");
            sb.AppendLine("echo Update complete! Restarting WhatsApp Automation...");
            sb.AppendLine($"start \"\" \"{currentExePath}\"");
            sb.AppendLine("timeout /t 2 /nobreak >nul");
            sb.AppendLine($"del /f /q \"{downloadedFilePath}\" >nul 2>&1");
            sb.AppendLine("del /f /q \"%~f0\" >nul 2>&1");
            sb.AppendLine("exit");

            File.WriteAllText(scriptPath, sb.ToString(), System.Text.Encoding.Default);

            var psi = new ProcessStartInfo
            {
                FileName = scriptPath,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            Process.Start(psi);

            // Terminate current application cleanly
            Application.Exit();
            Environment.Exit(0);
        }

        public static bool IsNewerVersion(string latestTag, string currentTag)
        {
            if (string.IsNullOrWhiteSpace(latestTag))
                return false;
            if (string.IsNullOrWhiteSpace(currentTag))
                return true;

            string cleanLatest = latestTag.TrimStart('v', 'V').Trim();
            string cleanCurrent = currentTag.TrimStart('v', 'V').Trim();

            // Extract numeric version parts (e.g. 1.0.1 or 1.0.1.0)
            var mLatest = Regex.Match(cleanLatest, @"^[0-9]+(\.[0-9]+)*");
            var mCurrent = Regex.Match(cleanCurrent, @"^[0-9]+(\.[0-9]+)*");

            string vLatStr = mLatest.Success ? mLatest.Value : cleanLatest;
            string vCurStr = mCurrent.Success ? mCurrent.Value : cleanCurrent;

            // Ensure at least 2 parts for Version.TryParse
            if (!vLatStr.Contains(".")) vLatStr += ".0";
            if (!vCurStr.Contains(".")) vCurStr += ".0";

            if (Version.TryParse(vLatStr, out var vLatest) && Version.TryParse(vCurStr, out var vCurrent))
            {
                return vLatest > vCurrent;
            }

            return !string.Equals(cleanLatest, cleanCurrent, StringComparison.OrdinalIgnoreCase);
        }
    }
}
