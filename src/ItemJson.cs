using System;

namespace MangoShop.Assets
{
    public class ItemJson
    {
        public long ItId { get; set; }
        public string ItName { get; set; } = String.Empty; 
        public long ItQty { get; set; }
        public long ItPrice { get; set; }
        public string ItCat { get; set; } = string.Empty;
    }
}