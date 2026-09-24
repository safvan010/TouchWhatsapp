using System;

namespace WhatsappAutomation.Models
{
    public class SalesItem
    {
        public string ItemName { get; set; } = string.Empty;
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Disc { get; set; }
        public double NetAmount { get; set; }
    }
}
