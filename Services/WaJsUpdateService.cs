using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WhatsappAutomation.Services
{
    public class WaJsUpdateService
    {
        private const string GitHubLatestApiUrl = "https://api.github.com/repos/wppconnect-team/wa-js/releases/latest";
        private const string DirectDownloadUrl = "https://github.com/wppconnect-team/wa-js/releases/latest/download/wppconnect-wa.js";

        private readonly string _localScriptPath;
        private readonly WhatsAppJsBridge _bridge;

        public WaJsUpdateService(WhatsAppJsBridge bridge, string scriptPath = null)
        {
            _bridge = bridge;
            _localScriptPath = scriptPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "wppconnect-wa.js");
        }

        public string GetLocalVersion()
        {
            try
            {
                if (File.Exists(_localScriptPath))
                {
                    using (var reader = new StreamReader(_localScriptPath))
                    {
                        string header = reader.ReadLine() ?? "";
                        string line2 = reader.ReadLine() ?? "";
                        string combined = header + " " + line2;

                        var match = Regex.Match(combined, @"wppconnect-team/wa-js\s+(v[0-9\.]+)");
                        if (match.Success)
                        {
                            return match.Groups[1].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[WaJsUpdateService] Error reading local version: {ex.Message}");
            }

            return "v4.6.0";
        }

        public async Task<(bool HasUpdate, string CurrentVersion, string LatestVersion, string DownloadUrl, string Error)> CheckForUpdatesAsync()
        {
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "WhatsAppAutomation-Client");

                    string json = await Task.Run(() => client.DownloadString(GitHubLatestApiUrl));
                    var root = JObject.Parse(json);

                    string latestTag = root["tag_name"]?.ToString() ?? "";
                    string currentVersion = GetLocalVersion();

                    string downloadUrl = DirectDownloadUrl;
                    var assets = root["assets"] as JArray;
                    if (assets != null)
                    {
                        foreach (var asset in assets)
                        {
                            if (string.Equals(asset["name"]?.ToString(), "wppconnect-wa.js", StringComparison.OrdinalIgnoreCase))
                            {
                                string url = asset["browser_download_url"]?.ToString();
                                if (!string.IsNullOrEmpty(url))
                                {
                                    downloadUrl = url;
                                    break;
                                }
                            }
                        }
                    }

                    bool hasUpdate = IsNewerVersion(latestTag, currentVersion);
                    return (hasUpdate, currentVersion, latestTag, downloadUrl, null);
                }
            }
            catch (Exception ex)
            {
                return (false, GetLocalVersion(), null, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Message, string NewVersion)> DownloadAndApplyUpdateAsync(string downloadUrl = null)
        {
            string url = downloadUrl ?? DirectDownloadUrl;

            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                string tempFile = _localScriptPath + ".tmp";
                string dir = Path.GetDirectoryName(_localScriptPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "WhatsAppAutomation-Client");
                    await Task.Run(() => client.DownloadFile(url, tempFile));
                }

                // Verify downloaded file is valid JS and non-empty (> 50KB)
                var fi = new FileInfo(tempFile);
                if (fi.Length < 50000)
                {
                    File.Delete(tempFile);
                    return (false, "Downloaded file appears corrupt or incomplete.", null);
                }

                string contentPreview = File.ReadAllText(tempFile).Substring(0, Math.Min(1000, (int)fi.Length));
                if (!contentPreview.Contains("wppconnect"))
                {
                    File.Delete(tempFile);
                    return (false, "Downloaded file does not match WA-JS signature.", null);
                }

                // Replace local file
                if (File.Exists(_localScriptPath))
                {
                    File.Delete(_localScriptPath);
                }
                File.Move(tempFile, _localScriptPath);

                // If running from bin folder during development, also update source folder assets if present
                try
                {
                    string sourceAsset = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "assets", "wppconnect-wa.js");
                    if (File.Exists(sourceAsset))
                    {
                        File.Copy(_localScriptPath, sourceAsset, true);
                    }
                }
                catch { }

                // Invalidate cached script in bridge so next injection uses the new version
                _bridge?.InvalidateScriptCache();

                string newVersion = GetLocalVersion();
                return (true, $"WA-JS updated to {newVersion} successfully!", newVersion);
            }
            catch (Exception ex)
            {
                return (false, $"Download failed: {ex.Message}", null);
            }
        }

        public static bool IsNewerVersion(string latestTag, string currentTag)
        {
            if (string.IsNullOrWhiteSpace(latestTag))
                return false;
            if (string.IsNullOrWhiteSpace(currentTag))
                return true;

            string cleanLatest = latestTag.TrimStart('v', 'V').Trim();
            string cleanCurrent = currentTag.TrimStart('v', 'V').Trim();

            if (Version.TryParse(cleanLatest, out var vLatest) && Version.TryParse(cleanCurrent, out var vCurrent))
            {
                return vLatest > vCurrent;
            }

            return !string.Equals(cleanLatest, cleanCurrent, StringComparison.OrdinalIgnoreCase);
        }
    }
}
