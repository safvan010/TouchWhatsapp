using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WhatsappAutomation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configure CefSharp settings
            var settings = new CefSettings();
            
            // Set persistent cache path so WhatsApp Web session / QR login is saved
            string cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "whatsapp_cache");
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            settings.CachePath = cachePath;
            settings.PersistSessionCookies = true;

            // WhatsApp Web requires a modern desktop Chrome user agent
            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36";

            // Optimize flags
            settings.CefCommandLineArgs.Add("disable-features", "CalculateNativeWinOcclusion");
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");

            // Initialize CefSharp
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            Application.Run(new MainForm());

            // Ensure CefSharp shuts down cleanly when the application exits
            if (Cef.IsInitialized == true)
            {
                Cef.Shutdown();
            }
        }
    }
}
