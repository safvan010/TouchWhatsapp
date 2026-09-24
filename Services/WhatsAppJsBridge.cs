using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CefSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WhatsappAutomation.Services
{
    public class WhatsAppJsBridge
    {
        private readonly string _scriptPath;
        private string _cachedScript;

        public WhatsAppJsBridge(string scriptPath = null)
        {
            if (string.IsNullOrEmpty(scriptPath))
            {
                _scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "wppconnect-wa.js");
            }
            else
            {
                _scriptPath = scriptPath;
            }
        }

        public void InvalidateScriptCache()
        {
            _cachedScript = null;
        }

        public async Task<bool> InjectWaJsAsync(IWebBrowser browser)
        {
            try
            {
                if (browser == null || !browser.CanExecuteJavascriptInMainFrame)
                    return false;

                if (_cachedScript == null)
                {
                    if (File.Exists(_scriptPath))
                    {
                        _cachedScript = File.ReadAllText(_scriptPath);
                    }
                    else
                    {
                        string fallback = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "assets", "wppconnect-wa.js");
                        if (File.Exists(fallback))
                        {
                            _cachedScript = File.ReadAllText(fallback);
                        }
                        else
                        {
                            throw new FileNotFoundException("Cannot find wppconnect-wa.js at: " + _scriptPath);
                        }
                    }
                }

                // Check if WPP is already present
                var checkResponse = await browser.EvaluateScriptAsync(@"
                    (function() {
                        return (typeof window.WPP !== 'undefined' && typeof window.WPP.chat !== 'undefined');
                    })()
                ");

                if (checkResponse.Success && checkResponse.Result is bool isAlreadyLoaded && isAlreadyLoaded)
                {
                    return true;
                }

                // Inject the script into the main frame
                browser.ExecuteScriptAsync(_cachedScript);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[InjectWaJsAsync] Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsReadyAsync(IWebBrowser browser)
        {
            try
            {
                if (browser == null || !browser.CanExecuteJavascriptInMainFrame)
                    return false;

                var response = await browser.EvaluateScriptAsync(@"
                    (function() {
                        if (typeof window.WPP === 'undefined') {
                            return false;
                        }

                        // wa-js v4+ properties
                        if (window.WPP.isReady === true || window.WPP.isFullReady === true) {
                            return true;
                        }

                        // If chat functions are already attached and available
                        if (window.WPP.chat && typeof window.WPP.chat.sendTextMessage === 'function') {
                            return true;
                        }

                        // Legacy fallback
                        if (window.WPP.webpack && window.WPP.webpack.isReady === true) {
                            return true;
                        }

                        return false;
                    })()
                ");

                if (response.Success && response.Result is bool isReady)
                {
                    return isReady;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsAuthenticatedAsync(IWebBrowser browser)
        {
            try
            {
                if (browser == null || !browser.CanExecuteJavascriptInMainFrame)
                    return false;

                var response = await browser.EvaluateScriptAsync(@"
                    (function() {
                        try {
                            if (typeof window.WPP === 'undefined' || !window.WPP.conn) {
                                return false;
                            }
                            
                            var conn = window.WPP.conn;
                            
                            // isMainLoaded is a function in WA-JS
                            if (typeof conn.isMainLoaded === 'function' && conn.isMainLoaded()) {
                                return true;
                            }
                            if (conn.isMainLoaded === true) {
                                return true;
                            }
                            if (typeof conn.isAuthenticated === 'function' && conn.isAuthenticated()) {
                                return true;
                            }
                            if (typeof conn.isMainReady === 'function' && conn.isMainReady()) {
                                return true;
                            }

                            return false;
                        } catch (e) {
                            return false;
                        }
                    })()
                ");

                if (response.Success && response.Result is bool isAuth)
                {
                    return isAuth;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetMyNumberAsync(IWebBrowser browser)
        {
            try
            {
                if (browser == null || !browser.CanExecuteJavascriptInMainFrame)
                    return null;

                var response = await browser.EvaluateScriptAsync(@"
                    (function() {
                        try {
                            if (typeof window.WPP !== 'undefined' && window.WPP.conn) {
                                var wid = typeof window.WPP.conn.getMyUserId === 'function' ? window.WPP.conn.getMyUserId() : null;
                                return wid ? wid.toString() : null;
                            }
                            return null;
                        } catch (e) {
                            return null;
                        }
                    })()
                ");

                if (response.Success && response.Result != null)
                {
                    return response.Result.ToString();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string SanitizePhoneNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return Regex.Replace(input, @"[^\d]", "");
        }

        public async Task<(bool Success, string Message)> SendMessageAsync(IWebBrowser browser, string rawPhone, string message)
        {
            try
            {
                if (browser == null || !browser.CanExecuteJavascriptInMainFrame)
                {
                    return (false, "WhatsApp Web is not loaded in the browser. Please ensure the page is active.");
                }

                string cleanNumber = SanitizePhoneNumber(rawPhone);
                if (string.IsNullOrEmpty(cleanNumber) || cleanNumber.Length < 6)
                {
                    return (false, "Please provide a valid numeric mobile number with country code (e.g., 919567366327).");
                }

                if (string.IsNullOrWhiteSpace(message))
                {
                    return (false, "Message content cannot be empty.");
                }

                // Ensure WA-JS is injected before sending
                bool isReady = await IsReadyAsync(browser);
                if (!isReady)
                {
                    await InjectWaJsAsync(browser);
                    for (int i = 0; i < 6; i++)
                    {
                        await Task.Delay(500);
                        if (await IsReadyAsync(browser))
                        {
                            isReady = true;
                            break;
                        }
                    }

                    if (!isReady)
                    {
                        return (false, "WhatsApp Web engine is not ready yet. Please ensure you have scanned the QR code and your chats are visible.");
                    }
                }

                string targetWid = cleanNumber.EndsWith("@c.us") ? cleanNumber : $"{cleanNumber}@c.us";
                string jsonChatId = JsonConvert.SerializeObject(targetWid);
                string jsonMessage = JsonConvert.SerializeObject(message);

                string jsCode = $@"
                    return (async function() {{
                        try {{
                            if (typeof window.WPP === 'undefined' || !window.WPP.chat || typeof window.WPP.chat.sendTextMessage !== 'function') {{
                                return JSON.stringify({{
                                    success: false,
                                    error: 'WhatsApp Web WA-JS engine is not ready. Please verify that WhatsApp is logged in.'
                                }});
                            }}

                            var to = {jsonChatId};
                            var text = {jsonMessage};

                            console.log('[WA-JS] Verifying contact exists:', to);

                            // 1. Query contact existence and obtain valid WID/LID
                            var contact = null;
                            try {{
                                if (window.WPP.contact && typeof window.WPP.contact.queryWidExists === 'function') {{
                                    contact = await window.WPP.contact.queryWidExists(to);
                                }} else if (window.WPP.contact && typeof window.WPP.contact.queryExists === 'function') {{
                                    contact = await window.WPP.contact.queryExists(to);
                                }}
                            }} catch (qErr) {{
                                console.warn('[WA-JS] Contact query warning:', qErr);
                            }}

                            var sendWid = (contact && contact.wid) ? contact.wid : to;
                            var sendLid = (contact && contact.lid) ? contact.lid : null;

                            console.log('[WA-JS] Target resolved:', sendWid, 'LID:', sendLid);

                            // 2. Open chat in WhatsApp Web UI so user sees the chat immediately
                            try {{
                                if (window.WPP.chat && typeof window.WPP.chat.openChatBottom === 'function') {{
                                    await window.WPP.chat.openChatBottom(sendWid);
                                }} else if (window.WPP.chat && typeof window.WPP.chat.find === 'function') {{
                                    await window.WPP.chat.find(sendWid);
                                }}
                            }} catch (openErr) {{
                                console.warn('[WA-JS] Open chat warning (will still attempt sending):', openErr);
                            }}

                            // 3. Send text message
                            var sendResult = null;
                            try {{
                                sendResult = await window.WPP.chat.sendTextMessage(sendWid, text, {{
                                    waitForAck: true
                                }});
                            }} catch (sendErr) {{
                                var errMsg = sendErr ? (sendErr.message || sendErr.toString()) : '';
                                console.warn('[WA-JS] sendTextMessage primary attempt failed:', errMsg);

                                // If failed due to LID requirement, try sending via LID
                                if (sendLid) {{
                                    console.log('[WA-JS] Retrying sendTextMessage with LID:', sendLid);
                                    sendResult = await window.WPP.chat.sendTextMessage(sendLid, text, {{
                                        waitForAck: true
                                    }});
                                }} else {{
                                    throw sendErr;
                                }}
                            }}

                            var msgId = 'sent';
                            if (sendResult) {{
                                if (sendResult.id) {{
                                    msgId = sendResult.id._serialized || sendResult.id.toString();
                                }} else if (sendResult.messageSendResult) {{
                                    msgId = sendResult.messageSendResult.toString();
                                }}
                            }}

                            return JSON.stringify({{
                                success: true,
                                messageId: msgId
                            }});
                        }} catch (err) {{
                            console.error('[WA-JS] Exception in SendMessage:', err);
                            return JSON.stringify({{
                                success: false,
                                error: err ? (err.message || err.toString()) : 'Unknown error occurred in browser context'
                            }});
                        }}
                    }})();
                ";

                var response = await browser.EvaluateScriptAsPromiseAsync(jsCode);

                if (!response.Success)
                {
                    string cefError = response.Message ?? "JavaScript execution failed in browser.";
                    return (false, $"Browser Script Error: {cefError}");
                }

                if (response.Result == null)
                {
                    return (false, "No response returned from browser script execution.");
                }

                string jsonStr = response.Result.ToString();
                var resultObj = JObject.Parse(jsonStr);

                bool isSuccess = resultObj["success"]?.Value<bool>() ?? false;
                if (isSuccess)
                {
                    string msgId = resultObj["messageId"]?.Value<string>() ?? "sent";
                    return (true, $"Message delivered successfully! (ID: {msgId})");
                }
                else
                {
                    string errorMsg = resultObj["error"]?.Value<string>() ?? "Failed to send message.";
                    return (false, errorMsg);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public static string GetMimeType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLowerInvariant();
            switch (ext)
            {
                case ".pdf": return "application/pdf";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".txt": return "text/plain";
                case ".csv": return "text/csv";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".gif": return "image/gif";
                case ".webp": return "image/webp";
                case ".mp4": return "video/mp4";
                case ".mp3": return "audio/mpeg";
                case ".zip": return "application/zip";
                case ".rar": return "application/x-rar-compressed";
                default: return "application/octet-stream";
            }
        }

        public async Task<(bool Success, string Message)> SendFileMessageAsync(
            IWebBrowser browser, 
            string rawPhone, 
            string filePath, 
            string caption = null)
        {
            try
            {
                if (browser == null || !browser.CanExecuteJavascriptInMainFrame)
                {
                    return (false, "WhatsApp Web is not loaded in the browser. Please ensure the page is active.");
                }

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    return (false, "The selected file does not exist: " + filePath);
                }

                string cleanNumber = SanitizePhoneNumber(rawPhone);
                if (string.IsNullOrEmpty(cleanNumber) || cleanNumber.Length < 6)
                {
                    return (false, "Please provide a valid numeric mobile number with country code (e.g., 919567366327).");
                }

                // Ensure WA-JS is ready
                bool isReady = await IsReadyAsync(browser);
                if (!isReady)
                {
                    await InjectWaJsAsync(browser);
                    for (int i = 0; i < 6; i++)
                    {
                        await Task.Delay(500);
                        if (await IsReadyAsync(browser))
                        {
                            isReady = true;
                            break;
                        }
                    }

                    if (!isReady)
                    {
                        return (false, "WhatsApp Web engine is not ready yet. Please ensure you are logged in and your chats are visible.");
                    }
                }

                string fileName = Path.GetFileName(filePath);
                string mimeType = GetMimeType(filePath);
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64 = Convert.ToBase64String(fileBytes);
                string dataUri = $"data:{mimeType};base64,{base64}";

                string fileType = "document";
                if (mimeType.StartsWith("image/"))
                    fileType = "image";
                else if (mimeType.StartsWith("video/"))
                    fileType = "video";
                else if (mimeType.StartsWith("audio/"))
                    fileType = "audio";

                string targetWid = cleanNumber.EndsWith("@c.us") ? cleanNumber : $"{cleanNumber}@c.us";
                string jsonChatId = JsonConvert.SerializeObject(targetWid);
                string jsonDataUri = JsonConvert.SerializeObject(dataUri);
                string jsonCaption = JsonConvert.SerializeObject(caption ?? "");
                string jsonFileName = JsonConvert.SerializeObject(fileName);
                string jsonFileType = JsonConvert.SerializeObject(fileType);

                string jsCode = $@"
                    return (async function() {{
                        try {{
                            if (typeof window.WPP === 'undefined' || !window.WPP.chat || typeof window.WPP.chat.sendFileMessage !== 'function') {{
                                return JSON.stringify({{
                                    success: false,
                                    error: 'WhatsApp Web WA-JS engine is not ready. Please verify that WhatsApp is logged in.'
                                }});
                            }}

                            var to = {jsonChatId};
                            var dataUri = {jsonDataUri};
                            var caption = {jsonCaption};
                            var fileName = {jsonFileName};
                            var fileType = {jsonFileType};

                            console.log('[WA-JS] Verifying contact exists for file send:', to);

                            var contact = null;
                            try {{
                                if (window.WPP.contact && typeof window.WPP.contact.queryWidExists === 'function') {{
                                    contact = await window.WPP.contact.queryWidExists(to);
                                }} else if (window.WPP.contact && typeof window.WPP.contact.queryExists === 'function') {{
                                    contact = await window.WPP.contact.queryExists(to);
                                }}
                            }} catch (qErr) {{
                                console.warn('[WA-JS] Contact query warning:', qErr);
                            }}

                            var sendWid = (contact && contact.wid) ? contact.wid : to;
                            var sendLid = (contact && contact.lid) ? contact.lid : null;

                            console.log('[WA-JS] Target resolved for file:', sendWid, 'LID:', sendLid);

                            // Open chat in UI
                            try {{
                                if (window.WPP.chat && typeof window.WPP.chat.openChatBottom === 'function') {{
                                    await window.WPP.chat.openChatBottom(sendWid);
                                }} else if (window.WPP.chat && typeof window.WPP.chat.find === 'function') {{
                                    await window.WPP.chat.find(sendWid);
                                }}
                            }} catch (openErr) {{
                                console.warn('[WA-JS] Open chat warning:', openErr);
                            }}

                            var options = {{
                                type: fileType,
                                caption: caption,
                                filename: fileName,
                                waitForAck: true
                            }};

                            var sendResult = null;
                            try {{
                                sendResult = await window.WPP.chat.sendFileMessage(sendWid, dataUri, options);
                            }} catch (sendErr) {{
                                var errMsg = sendErr ? (sendErr.message || sendErr.toString()) : '';
                                console.warn('[WA-JS] sendFileMessage primary attempt failed:', errMsg);

                                if (sendLid) {{
                                    console.log('[WA-JS] Retrying sendFileMessage with LID:', sendLid);
                                    sendResult = await window.WPP.chat.sendFileMessage(sendLid, dataUri, options);
                                }} else {{
                                    throw sendErr;
                                }}
                            }}

                            var msgId = 'sent';
                            if (sendResult) {{
                                if (sendResult.id) {{
                                    msgId = sendResult.id._serialized || sendResult.id.toString();
                                }} else if (sendResult.messageSendResult) {{
                                    msgId = sendResult.messageSendResult.toString();
                                }}
                            }}

                            return JSON.stringify({{
                                success: true,
                                messageId: msgId
                            }});
                        }} catch (err) {{
                            console.error('[WA-JS] Exception in sendFileMessage:', err);
                            return JSON.stringify({{
                                success: false,
                                error: err ? (err.message || err.toString()) : 'Unknown error occurred while sending file'
                            }});
                        }}
                    }})();
                ";

                var response = await browser.EvaluateScriptAsPromiseAsync(jsCode);

                if (!response.Success)
                {
                    string cefError = response.Message ?? "JavaScript execution failed in browser.";
                    return (false, $"Browser Script Error: {cefError}");
                }

                if (response.Result == null)
                {
                    return (false, "No response returned from browser script execution.");
                }

                string jsonStr = response.Result.ToString();
                var resultObj = JObject.Parse(jsonStr);

                bool isSuccess = resultObj["success"]?.Value<bool>() ?? false;
                if (isSuccess)
                {
                    string msgId = resultObj["messageId"]?.Value<string>() ?? "sent";
                    return (true, $"File delivered successfully! (ID: {msgId})");
                }
                else
                {
                    string errorMsg = resultObj["error"]?.Value<string>() ?? "Failed to send file.";
                    return (false, errorMsg);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
