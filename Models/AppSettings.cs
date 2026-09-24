using System;
using System.IO;
using Newtonsoft.Json;

namespace WhatsappAutomation.Models
{
    public class AppSettings
    {
        public string MessageTemplate { get; set; } = 
@"Hai {CustomerName},
Thank you for your purchase!
Bill No: {VoucherNo}
Date: {SoldDate}
Bill Amount: {GrandTotal}";

        public bool SendInvoicePdf { get; set; } = false;
        public string PaperSize { get; set; } = "A4"; // "A4" or "Thermal3Inch"

        public string Header1_ShopName { get; set; } = "MY STORE / SUPERMARKET";
        public string Header2_Address { get; set; } = "Main Road, Commercial Complex";
        public string Header3_Contact { get; set; } = "Tel: 9876543210 | GSTIN: 32XXXXX0000X1Z1";

        public bool AutoUpdateWaJs { get; set; } = true;

        // Application Software Updater
        public bool AutoCheckAppUpdates { get; set; } = true;
        public string AppUpdateSource { get; set; } = "safvan010/TouchWhatsapp";

        private static string GetAppDirectory()
        {
            try
            {
                string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (!string.IsNullOrEmpty(loc))
                    return Path.GetDirectoryName(loc);
            }
            catch { }
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private static readonly string SettingsFilePath = Path.Combine(GetAppDirectory(), "appsettings.json");

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    if (settings != null)
                        return settings;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppSettings] Error loading settings: {ex.Message}");
            }

            var defaultSettings = new AppSettings();
            defaultSettings.Save();
            return defaultSettings;
        }

        public void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppSettings] Error saving settings: {ex.Message}");
            }
        }
    }
}
