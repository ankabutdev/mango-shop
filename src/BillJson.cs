using System;

namespace MangoShop
{
    public class BillJson
    {
        public long BId { get; set; }
        public string EName { get; set; } = string.Empty;
        public string ClientName { get; set; } = String.Empty;
        public long Amount { get; set; }
    }
}