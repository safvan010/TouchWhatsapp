using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using WhatsappAutomation.Models;
using WhatsappAutomation.Services;

namespace WhatsappAutomation
{
    public partial class MainForm : Form
    {
        private ChromiumWebBrowser _browser;
        private readonly WhatsAppJsBridge _bridge;
        private readonly SqlDatabaseService _dbService;
        private readonly WaJsUpdateService _updateService;
        private readonly AppUpdateService _appUpdateService;
        private AppSettings _appSettings;

        private bool _isEngineReady = false;
        private bool _isAutoSendRunning = false;
        private bool _isProcessingBatch = false;

        public MainForm()
        {
            InitializeComponent();
            _bridge = new WhatsAppJsBridge();
            _dbService = new SqlDatabaseService();
            _updateService = new WaJsUpdateService(_bridge);
            _appUpdateService = new AppUpdateService();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // System Tray icon setup
            try
            {
                trayIcon.Icon = SystemIcons.Application;
            }
            catch { }

            // Load Database Config into UI
            txtDbInstance.Text = _dbService.InstanceName;
            txtDbName.Text = _dbService.DatabaseName;
            dtpFilterDate.Value = DateTime.Today;

            // Load Application Settings into UI
            _appSettings = AppSettings.Load();
            txtMsgTemplate.Text = _appSettings.MessageTemplate;
            chkSendPdf.Checked = _appSettings.SendInvoicePdf;
            rbPaperA4.Checked = _appSettings.PaperSize == "A4";
            rbPaperThermal.Checked = _appSettings.PaperSize == "Thermal3Inch";
            txtHeader1.Text = _appSettings.Header1_ShopName;
            txtHeader2.Text = _appSettings.Header2_Address;
            txtHeader3.Text = _appSettings.Header3_Contact;
            chkAutoUpdateWaJs.Checked = _appSettings.AutoUpdateWaJs;
            lblWaJsVersion.Text = $"Local Version: {_updateService.GetLocalVersion()}";

            // Load Application Update Config into UI
            chkAutoCheckAppUpdates.Checked = _appSettings.AutoCheckAppUpdates;
            txtAppUpdateSource.Text = _appSettings.AppUpdateSource;
            lblAppVersion.Text = $"App Version: {_appUpdateService.GetCurrentVersion()}";

            AppendAutoLog("Application started.");
            AppendAutoLog($"Loaded SQL Config: Instance={_dbService.InstanceName}, DB={_dbService.DatabaseName}");
            AppendAutoLog($"Loaded Settings: SendInvoicePdf={_appSettings.SendInvoicePdf}, PaperSize={_appSettings.PaperSize}, AutoUpdateWaJs={_appSettings.AutoUpdateWaJs}, AutoCheckAppUpdates={_appSettings.AutoCheckAppUpdates}");

            btnSend.Enabled = false;
            InitializeBrowser();
            statusTimer.Start();

            // Check for WA-JS updates in background if enabled
            if (_appSettings.AutoUpdateWaJs)
            {
                CheckWaJsUpdatesInBackground(false);
            }

            // Check for Application updates in background if enabled
            if (_appSettings.AutoCheckAppUpdates && !string.IsNullOrWhiteSpace(_appSettings.AppUpdateSource))
            {
                CheckAppUpdatesInBackground(false);
            }
        }

        private void InitializeBrowser()
        {
            UpdateStatus("Loading WhatsApp Web...", Color.FromArgb(255, 243, 205), Color.FromArgb(133, 100, 4));

            _browser = new ChromiumWebBrowser("https://web.whatsapp.com")
            {
                Dock = DockStyle.Fill
            };

            _browser.FrameLoadEnd += Browser_FrameLoadEnd;
            _browser.LoadingStateChanged += Browser_LoadingStateChanged;
            _browser.ConsoleMessage += Browser_ConsoleMessage;

            pnlBrowserHost.Controls.Add(_browser);
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1500);
                    await TryInjectWaJsAsync();
                });
            }
        }

        private void Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            if (e.Message.Contains("[WA-JS]"))
            {
                AppendAutoLog(e.Message);
            }
            else if (e.Level == LogSeverity.Error && !e.Message.Contains("favicon"))
            {
                System.Diagnostics.Debug.WriteLine($"[Browser Error] {e.Message}");
            }
        }

        private async void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                await Task.Delay(2000);
                await TryInjectWaJsAsync();
            }
        }

        private async Task TryInjectWaJsAsync()
        {
            try
            {
                if (_browser == null || !_browser.CanExecuteJavascriptInMainFrame)
                    return;

                await _bridge.InjectWaJsAsync(_browser);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error injecting WA-JS: {ex.Message}");
            }
        }

        private async void statusTimer_Tick(object sender, EventArgs e)
        {
            if (_browser == null || !_browser.CanExecuteJavascriptInMainFrame)
                return;

            try
            {
                bool isAuth = await _bridge.IsAuthenticatedAsync(_browser);
                bool isReady = await _bridge.IsReadyAsync(_browser);

                if (!isReady && isAuth)
                {
                    await TryInjectWaJsAsync();
                    isReady = await _bridge.IsReadyAsync(_browser);
                }

                if (isAuth || isReady)
                {
                    if (!_isEngineReady)
                    {
                        _isEngineReady = true;
                        string myAccount = await _bridge.GetMyNumberAsync(_browser);
                        string info = string.IsNullOrEmpty(myAccount) ? "" : $" ({myAccount})";
                        AppendAutoLog($"WhatsApp Web connected & authenticated!{info}");
                        AppendManualLog($"WhatsApp Web connected & authenticated!{info}");
                    }

                    UpdateStatus("Connected & Ready to Send Messages!", Color.FromArgb(212, 237, 218), Color.FromArgb(21, 87, 36));
                    btnSend.Enabled = true;
                }
                else
                {
                    _isEngineReady = false;
                    btnSend.Enabled = false;
                    UpdateStatus("Please Scan QR Code with your phone to log in...", Color.FromArgb(255, 243, 205), Color.FromArgb(133, 100, 4));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Status check error: {ex.Message}");
            }
        }

        private void UpdateStatus(string message, Color backColor, Color textColor)
        {
            if (lblStatusBadge.InvokeRequired)
            {
                lblStatusBadge.Invoke(new Action(() => UpdateStatus(message, backColor, textColor)));
                return;
            }

            lblStatusBadge.Text = "Status: " + message;
            lblStatusBadge.BackColor = backColor;
            lblStatusBadge.ForeColor = textColor;
        }

        private void UpdateAutoStatus(string text)
        {
            if (lblAutoStatus.InvokeRequired)
            {
                lblAutoStatus.Invoke(new Action(() => UpdateAutoStatus(text)));
                return;
            }

            lblAutoStatus.Text = "Auto-Send Status: " + text;
        }

        private void AppendAutoLog(string message)
        {
            if (txtAutoLog.InvokeRequired)
            {
                txtAutoLog.Invoke(new Action(() => AppendAutoLog(message)));
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtAutoLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
        }

        private void AppendManualLog(string message)
        {
            if (txtManualLog.InvokeRequired)
            {
                txtManualLog.Invoke(new Action(() => AppendManualLog(message)));
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtManualLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
        }

        #region SQL Database & Auto-Send Logic

        private void btnSaveDbConfig_Click(object sender, EventArgs e)
        {
            try
            {
                _dbService.SaveConfig(txtDbInstance.Text, txtDbName.Text);
                AppendAutoLog("Saved database configuration to dbconfig.txt.");
                MessageBox.Show("Database configuration saved successfully to dbconfig.txt!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppendAutoLog($"[ERROR] Failed saving dbconfig.txt: {ex.Message}");
                MessageBox.Show(ex.Message, "Error Saving Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTestDbConn_Click(object sender, EventArgs e)
        {
            _dbService.InstanceName = txtDbInstance.Text.Trim();
            _dbService.DatabaseName = txtDbName.Text.Trim();

            btnTestDbConn.Enabled = false;
            btnTestDbConn.Text = "Testing...";
            AppendAutoLog($"Testing connection to '{_dbService.InstanceName}' / '{_dbService.DatabaseName}'...");

            try
            {
                var (success, msg) = await _dbService.TestConnectionAsync();
                if (success)
                {
                    AppendAutoLog($"[SUCCESS] {msg}");
                    MessageBox.Show(msg, "SQL Server Connected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AppendAutoLog($"[FAILED] {msg}");
                    MessageBox.Show(msg, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnTestDbConn.Enabled = true;
                btnTestDbConn.Text = "Test Connection";
            }
        }

        private void chkSpecificDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFilterDate.Enabled = chkSpecificDate.Checked;
        }

        private void btnToggleAutoSend_Click(object sender, EventArgs e)
        {
            if (!_isAutoSendRunning)
            {
                StartAutoSend();
            }
            else
            {
                StopAutoSend();
            }
        }

        private void StartAutoSend()
        {
            _isAutoSendRunning = true;
            btnToggleAutoSend.Text = "⏹ Stop Auto-Send";
            btnToggleAutoSend.BackColor = Color.FromArgb(220, 53, 69);
            trayToggleAutoItem.Text = "Stop Auto-Send";

            _dbService.InstanceName = txtDbInstance.Text.Trim();
            _dbService.DatabaseName = txtDbName.Text.Trim();

            AppendAutoLog("=== Auto-Send Started (Checking every 10 seconds) ===");
            UpdateAutoStatus("Running (Checking now...)");

            // Execute initial check immediately
            TriggerPollBatch();
        }

        private void StopAutoSend()
        {
            _isAutoSendRunning = false;
            sqlPollTimer.Stop();

            btnToggleAutoSend.Text = "▶ Start Auto-Send (Every 10s)";
            btnToggleAutoSend.BackColor = Color.FromArgb(37, 211, 102);
            trayToggleAutoItem.Text = "Start Auto-Send";

            AppendAutoLog("=== Auto-Send Stopped by User ===");
            UpdateAutoStatus("Stopped");
        }

        private void sqlPollTimer_Tick(object sender, EventArgs e)
        {
            TriggerPollBatch();
        }

        private async void TriggerPollBatch()
        {
            if (!_isAutoSendRunning || _isProcessingBatch)
                return;

            _isProcessingBatch = true;
            sqlPollTimer.Stop(); // Stop timer while processing batch

            try
            {
                // Verify WhatsApp Web login
                bool isAuth = await _bridge.IsAuthenticatedAsync(_browser);
                if (!isAuth)
                {
                    AppendAutoLog("WhatsApp Web is not authenticated yet. Waiting for QR scan / login...");
                    UpdateAutoStatus("Waiting for WhatsApp Web login...");
                    return;
                }

                DateTime? filterDate = chkSpecificDate.Checked ? dtpFilterDate.Value.Date : (DateTime?)null;
                string dateLog = filterDate.HasValue ? filterDate.Value.ToString("yyyy-MM-dd") : "Today (GETDATE())";

                var records = await _dbService.GetPendingSalesAsync(filterDate);

                if (records.Count > 0)
                {
                    AppendAutoLog($"Found {records.Count} unsent sales records for {dateLog}.");
                    UpdateAutoStatus($"Sending {records.Count} records...");

                    for (int i = 0; i < records.Count; i++)
                    {
                        if (!_isAutoSendRunning)
                        {
                            AppendAutoLog("Auto-send was stopped during batch.");
                            break;
                        }

                        var rec = records[i];
                        string cleanMobile = SqlDatabaseService.NormalizeIndianMobile(rec.CustomerMobile);

                        if (string.IsNullOrEmpty(cleanMobile) || cleanMobile.Length < 10)
                        {
                            AppendAutoLog($"[SKIP] Invalid CustomerMobile '{rec.CustomerMobile}' for Voucher #{rec.VoucherNo} (SI: {rec.SI}).");
                            continue;
                        }

                        string message = SqlDatabaseService.FormatSalesMessage(rec, _appSettings?.MessageTemplate, _appSettings?.Header1_ShopName);

                        bool sendSuccess = false;
                        string resultMsg = string.Empty;

                        if (_appSettings != null && _appSettings.SendInvoicePdf)
                        {
                            AppendAutoLog($"[{i + 1}/{records.Count}] Generating {_appSettings.PaperSize} bill PDF for Voucher #{rec.VoucherNo} (SI: {rec.SI})...");
                            try
                            {
                                var items = await _dbService.GetSalesItemsAsync(rec.SI);
                                string pdfPath = InvoicePdfService.GenerateInvoicePdf(rec, items, _appSettings);

                                AppendAutoLog($"Sending PDF bill with caption to {rec.CustomerName} ({cleanMobile})...");
                                var res = await _bridge.SendFileMessageAsync(_browser, cleanMobile, pdfPath, message);
                                sendSuccess = res.Success;
                                resultMsg = res.Message;
                            }
                            catch (Exception pdfEx)
                            {
                                AppendAutoLog($"[PDF ERROR] Failed to generate/send PDF: {pdfEx.Message}. Falling back to text message.");
                                var res = await _bridge.SendMessageAsync(_browser, cleanMobile, message);
                                sendSuccess = res.Success;
                                resultMsg = res.Message;
                            }
                        }
                        else
                        {
                            AppendAutoLog($"[{i + 1}/{records.Count}] Sending message to {rec.CustomerName} ({cleanMobile}) - Bill: {rec.GrandTotal:N2}...");
                            var res = await _bridge.SendMessageAsync(_browser, cleanMobile, message);
                            sendSuccess = res.Success;
                            resultMsg = res.Message;
                        }

                        if (sendSuccess)
                        {
                            bool updated = await _dbService.UpdateTokenNumberAsync(rec.SI, "1");
                            AppendAutoLog($"[SUCCESS] Sent to {cleanMobile}! TokenNumber updated: {updated}");
                        }
                        else
                        {
                            AppendAutoLog($"[FAILED] Voucher #{rec.VoucherNo} to {cleanMobile}: {resultMsg}");
                        }

                        // Anti-flood delay: wait 2.5 seconds before next message
                        await Task.Delay(2500);
                    }

                    AppendAutoLog("Batch completed.");
                }
                else
                {
                    UpdateAutoStatus("Idle - No pending sales found");
                }
            }
            catch (Exception ex)
            {
                AppendAutoLog($"[DB ERROR] {ex.Message}");
                UpdateAutoStatus("Database error occurred");
            }
            finally
            {
                _isProcessingBatch = false;
                if (_isAutoSendRunning)
                {
                    sqlPollTimer.Start(); // Resume 10-second timer
                    UpdateAutoStatus("Running (Next check in 10s)");
                }
            }
        }

        #endregion

        #region Manual Send Logic

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
                AppendManualLog($"Selected file: {Path.GetFileName(openFileDialog.FileName)}");
                btnSend.Text = "Send Document";
            }
        }

        private void btnClearFile_Click(object sender, EventArgs e)
        {
            txtFilePath.Clear();
            btnSend.Text = "Send Message";
            AppendManualLog("Document attachment cleared.");
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string phone = txtMobileNo.Text.Trim();
            string message = txtMessage.Text.Trim();
            string selectedFile = txtFilePath.Text.Trim();

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter a mobile number with country code (e.g., 919567366327).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMobileNo.Focus();
                return;
            }

            bool hasFile = !string.IsNullOrEmpty(selectedFile) && File.Exists(selectedFile);
            if (!hasFile && string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Please enter a message or select a file to send.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMessage.Focus();
                return;
            }

            btnSend.Enabled = false;
            btnSend.Text = "Sending...";

            try
            {
                bool success;
                string resultMsg;

                if (hasFile)
                {
                    AppendManualLog($"Sending document '{Path.GetFileName(selectedFile)}' to: {phone}...");
                    (success, resultMsg) = await _bridge.SendFileMessageAsync(_browser, phone, selectedFile, message);
                }
                else
                {
                    AppendManualLog($"Sending text message to: {phone}...");
                    (success, resultMsg) = await _bridge.SendMessageAsync(_browser, phone, message);
                }

                if (success)
                {
                    AppendManualLog($"[SUCCESS] {resultMsg}");
                    MessageBox.Show(resultMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMessage.Clear();
                    txtFilePath.Clear();
                }
                else
                {
                    AppendManualLog($"[FAILED] {resultMsg}");
                    MessageBox.Show(resultMsg, "Send Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AppendManualLog($"[EXCEPTION] {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSend.Enabled = true;
                btnSend.Text = !string.IsNullOrEmpty(txtFilePath.Text) ? "Send Document" : "Send Message";
            }
        }

        #endregion

        #region Bottom Buttons & Navigation

        private async void btnInjectWaJs_Click(object sender, EventArgs e)
        {
            AppendAutoLog("Manual re-injection of WA-JS requested.");
            await TryInjectWaJsAsync();
            bool isReady = await _bridge.IsReadyAsync(_browser);
            bool isAuth = await _bridge.IsAuthenticatedAsync(_browser);
            AppendAutoLog($"WA-JS Status: isReady = {isReady}, isAuthenticated = {isAuth}");
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            _isEngineReady = false;
            AppendAutoLog("Reloading WhatsApp Web...");
            _browser?.Reload(ignoreCache: false);
        }

        #endregion

        #region System Tray & Minimize to Background

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayIcon.ShowBalloonTip(1500, "WhatsApp Automation", "Running in the background. Double-click tray icon to open.", ToolTipIcon.Info);
            }
        }

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void trayOpenItem_Click(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void trayToggleAutoItem_Click(object sender, EventArgs e)
        {
            btnToggleAutoSend_Click(sender, e);
        }

        private void trayExitItem_Click(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Prompt or minimize to tray if auto-send is running
                if (_isAutoSendRunning)
                {
                    var result = MessageBox.Show(
                        "Auto-Send is currently running.\n\nDo you want to minimize to the background tray instead of exiting?",
                        "Running in Background",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        e.Cancel = true;
                        WindowState = FormWindowState.Minimized;
                        return;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            statusTimer.Stop();
            sqlPollTimer.Stop();
            trayIcon.Visible = false;
        }

        #endregion

        #region Settings Tab Logic

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (_appSettings == null)
                    _appSettings = new AppSettings();

                _appSettings.MessageTemplate = txtMsgTemplate.Text;
                _appSettings.SendInvoicePdf = chkSendPdf.Checked;
                _appSettings.PaperSize = rbPaperThermal.Checked ? "Thermal3Inch" : "A4";
                _appSettings.Header1_ShopName = txtHeader1.Text.Trim();
                _appSettings.Header2_Address = txtHeader2.Text.Trim();
                _appSettings.Header3_Contact = txtHeader3.Text.Trim();
                _appSettings.AutoUpdateWaJs = chkAutoUpdateWaJs.Checked;
                _appSettings.AutoCheckAppUpdates = chkAutoCheckAppUpdates.Checked;
                _appSettings.AppUpdateSource = txtAppUpdateSource.Text.Trim();
                _appSettings.Save();

                lblSettingsStatus.Text = "Settings saved successfully to appsettings.json!";
                lblSettingsStatus.ForeColor = Color.FromArgb(40, 167, 69);
                AppendAutoLog("Settings updated and saved to appsettings.json.");
            }
            catch (Exception ex)
            {
                lblSettingsStatus.Text = $"Error: {ex.Message}";
                lblSettingsStatus.ForeColor = Color.Red;
            }
        }

        private void btnCheckWaJsUpdate_Click(object sender, EventArgs e)
        {
            btnCheckWaJsUpdate.Enabled = false;
            CheckWaJsUpdatesInBackground(true);
        }

        private void CheckWaJsUpdatesInBackground(bool isManual)
        {
            Task.Run(async () =>
            {
                try
                {
                    UpdateWaJsStatusUI("Checking GitHub...", Color.FromArgb(133, 100, 4));
                    AppendAutoLog($"[WA-JS] Checking for updates (Local: {_updateService.GetLocalVersion()})...");

                    var (hasUpdate, currentVer, latestVer, downloadUrl, error) = await _updateService.CheckForUpdatesAsync();

                    if (!string.IsNullOrEmpty(error))
                    {
                        AppendAutoLog($"[WA-JS] Update check skipped: {error}");
                        UpdateWaJsStatusUI("Offline / GitHub unreachable", Color.Gray);
                        if (isManual)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Could not check for WA-JS updates:\n{error}", "WA-JS Update Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                        }
                        return;
                    }

                    if (hasUpdate)
                    {
                        AppendAutoLog($"[WA-JS] New update available: {latestVer} (Current: {currentVer}). Downloading...");
                        UpdateWaJsStatusUI($"Downloading {latestVer}...", Color.FromArgb(0, 123, 255));

                        var (success, msg, newVer) = await _updateService.DownloadAndApplyUpdateAsync(downloadUrl);
                        if (success)
                        {
                            AppendAutoLog($"[WA-JS] {msg}");
                            UpdateWaJsStatusUI($"Updated to {newVer}", Color.FromArgb(40, 167, 69));
                            this.Invoke(new Action(() =>
                            {
                                lblWaJsVersion.Text = $"Local Version: {newVer}";
                                if (isManual)
                                {
                                    MessageBox.Show($"WA-JS has been updated to {newVer} successfully!\nIt will be used automatically for all WhatsApp sessions.", "WA-JS Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }));
                        }
                        else
                        {
                            AppendAutoLog($"[WA-JS ERROR] {msg}");
                            UpdateWaJsStatusUI("Update failed", Color.Red);
                            if (isManual)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    MessageBox.Show($"WA-JS update failed:\n{msg}", "WA-JS Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }));
                            }
                        }
                    }
                    else
                    {
                        AppendAutoLog($"[WA-JS] WA-JS is already up to date ({currentVer}).");
                        UpdateWaJsStatusUI("Up to date", Color.FromArgb(40, 167, 69));
                        if (isManual)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"WA-JS is already up to date ({currentVer}).", "WA-JS Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[WA-JS] Update task error: {ex.Message}");
                    UpdateWaJsStatusUI("Error checking update", Color.Red);
                }
                finally
                {
                    this.Invoke(new Action(() =>
                    {
                        btnCheckWaJsUpdate.Enabled = true;
                    }));
                }
            });
        }

        private void UpdateWaJsStatusUI(string text, Color color)
        {
            if (lblWaJsStatus.InvokeRequired)
            {
                lblWaJsStatus.Invoke(new Action(() => UpdateWaJsStatusUI(text, color)));
                return;
            }

            lblWaJsStatus.Text = $"Status: {text}";
            lblWaJsStatus.ForeColor = color;
        }

        #endregion

        #region Application Updates

        private void btnCheckAppUpdate_Click(object sender, EventArgs e)
        {
            string source = txtAppUpdateSource.Text.Trim();
            if (string.IsNullOrWhiteSpace(source))
            {
                MessageBox.Show(
                    "Please enter an Update Source before checking for updates.\n\n" +
                    "Examples:\n" +
                    "• GitHub Repository: owner/repo (e.g. safvan/whatsapp-automation)\n" +
                    "• Manifest JSON URL: https://myserver.com/version.json\n\n" +
                    "After entering, click 'Save Settings' and try again.",
                    "Update Source Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtAppUpdateSource.Focus();
                return;
            }

            btnCheckAppUpdate.Enabled = false;
            CheckAppUpdatesInBackground(true);
        }

        private void CheckAppUpdatesInBackground(bool isManual)
        {
            Task.Run(async () =>
            {
                try
                {
                    string source = "";
                    this.Invoke(new Action(() =>
                    {
                        source = txtAppUpdateSource.Text.Trim();
                    }));

                    if (string.IsNullOrWhiteSpace(source))
                    {
                        source = _appSettings?.AppUpdateSource ?? "";
                    }

                    if (string.IsNullOrWhiteSpace(source))
                    {
                        if (isManual)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show("Please configure an Update Source (GitHub repo or JSON URL) in Settings first.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                        }
                        return;
                    }

                    UpdateAppStatusUI("Checking for updates...", Color.FromArgb(133, 100, 4));
                    AppendAutoLog($"[AppUpdater] Checking for software updates (Source: {source}, Local: {_appUpdateService.GetCurrentVersion()})...");

                    var info = await _appUpdateService.CheckForUpdatesAsync(source);

                    if (!string.IsNullOrEmpty(info.Error))
                    {
                        AppendAutoLog($"[AppUpdater] Check error: {info.Error}");
                        UpdateAppStatusUI("Error checking updates", Color.Red);
                        if (isManual)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Could not check for application updates:\n{info.Error}", "Update Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                        }
                        return;
                    }

                    if (info.HasUpdate)
                    {
                        AppendAutoLog($"[AppUpdater] New software version available: {info.LatestVersion} (Current: {info.CurrentVersion})");
                        UpdateAppStatusUI($"New version {info.LatestVersion} available!", Color.FromArgb(0, 123, 255));

                        DialogResult result = DialogResult.No;
                        this.Invoke(new Action(() =>
                        {
                            string notes = string.IsNullOrWhiteSpace(info.ReleaseNotes) ? "Bug fixes and performance improvements." : info.ReleaseNotes;
                            string prompt = $"A new version of WhatsApp Automation is available!\n\n" +
                                            $"Current Version: {info.CurrentVersion}\n" +
                                            $"New Version:     {info.LatestVersion}\n\n" +
                                            $"What's New in this update:\n-----------------------------------\n{notes}\n-----------------------------------\n\n" +
                                            $"Do you want to download and install this update now?";

                            result = MessageBox.Show(prompt, "Software Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }));

                        if (result == DialogResult.Yes)
                        {
                            await DownloadAndInstallAppUpdateAsync(info);
                        }
                    }
                    else
                    {
                        AppendAutoLog($"[AppUpdater] Application is up to date ({info.CurrentVersion}).");
                        UpdateAppStatusUI("Application is up to date", Color.FromArgb(40, 167, 69));
                        if (isManual)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"WhatsApp Automation is already up to date ({info.CurrentVersion}).", "No Updates Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[AppUpdater] Error: {ex.Message}");
                    UpdateAppStatusUI("Update check error", Color.Red);
                }
                finally
                {
                    this.Invoke(new Action(() =>
                    {
                        btnCheckAppUpdate.Enabled = true;
                    }));
                }
            });
        }

        private async Task DownloadAndInstallAppUpdateAsync(UpdateInfo info)
        {
            this.Invoke(new Action(() =>
            {
                prgAppUpdate.Value = 0;
                prgAppUpdate.Visible = true;
                lblAppUpdateProgress.Text = "Starting download...";
                lblAppUpdateProgress.Visible = true;
                btnCheckAppUpdate.Enabled = false;
            }));

            UpdateAppStatusUI($"Downloading {info.LatestVersion}...", Color.FromArgb(0, 123, 255));
            AppendAutoLog($"[AppUpdater] Downloading update package from: {info.DownloadUrl}");

            var (success, downloadedPath, error) = await _appUpdateService.DownloadUpdateAsync(
                info.DownloadUrl, 
                info.FileName, 
                (pct, rcv, total) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        prgAppUpdate.Value = Math.Max(0, Math.Min(100, pct));
                        if (total > 0)
                        {
                            lblAppUpdateProgress.Text = $"{pct}% ({(rcv / 1048576.0):F1} MB / {(total / 1048576.0):F1} MB)";
                        }
                        else
                        {
                            lblAppUpdateProgress.Text = $"{pct}% ({(rcv / 1048576.0):F1} MB)";
                        }
                    }));
                });

            if (!success || string.IsNullOrEmpty(downloadedPath))
            {
                AppendAutoLog($"[AppUpdater] Download failed: {error}");
                UpdateAppStatusUI("Download failed", Color.Red);
                this.Invoke(new Action(() =>
                {
                    prgAppUpdate.Visible = false;
                    lblAppUpdateProgress.Visible = false;
                    btnCheckAppUpdate.Enabled = true;
                    MessageBox.Show($"Failed to download update:\n{error}", "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
                return;
            }

            AppendAutoLog($"[AppUpdater] Download complete ({downloadedPath}). Launching installer and restarting...");
            UpdateAppStatusUI("Applying update...", Color.FromArgb(40, 167, 69));

            this.Invoke(new Action(() =>
            {
                MessageBox.Show(
                    "The update was downloaded successfully!\n\n" +
                    "WhatsApp Automation will now close, apply the updated files, and automatically restart.",
                    "Update Ready",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                try
                {
                    _appUpdateService.ApplyUpdateAndRestart(downloadedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to apply update: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    prgAppUpdate.Visible = false;
                    lblAppUpdateProgress.Visible = false;
                    btnCheckAppUpdate.Enabled = true;
                }
            }));
        }

        private void UpdateAppStatusUI(string text, Color color)
        {
            if (lblAppUpdateStatus.InvokeRequired)
            {
                lblAppUpdateStatus.Invoke(new Action(() => UpdateAppStatusUI(text, color)));
                return;
            }

            lblAppUpdateStatus.Text = $"Status: {text}";
            lblAppUpdateStatus.ForeColor = color;
        }

        #endregion
    }
}
