using System;
using SQLite;

namespace Q.Models
{
    public class Tbl_Sku
    {
        [PrimaryKey]
        public double SKU_Barcode { get; set; }
        public string SKU_Name { get; set; }
        public string SKU_Detail { get; set; }
        public string SKU_Role { get; set; }
        public DateTime? SKU_SyncFlag { get; set; }
        public DateTime? SKU_DeleteFlag { get; set; }
    }
}
