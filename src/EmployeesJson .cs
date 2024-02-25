using System;

namespace MangoShop.Assets
{
    public class EmployeesJson
    {
        public long EmpID { get; set; }
        public string EmpName { get; set; } = String.Empty; 
        public long EmpPhone { get; set; } 
        public string EmpAdd { get; set; } = String.Empty;
        public string EmpPass { get; set; } = String.Empty;
        public long ItQty { get; internal set; }
    }
}