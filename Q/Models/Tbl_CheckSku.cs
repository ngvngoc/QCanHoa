using System;
using SQLite;

namespace Q.Models
{
    public class Tbl_CheckSku
    {
        [PrimaryKey]
        public string CS_ID { get; set; }
        public double? SKU_Barcode { get; set; }
        public string ER_ID { get; set; }
        public string SHOP_ID { get; set; }
        public string CS_Warning { get; set; }
        public string CS_ExpiryProduction { get; set; }
        public string CS_ExpiryProductionDate { get; set; }
        public string CS_Image { get; set; }
        public DateTime? CS_SyncFlag { get; set; }
        public DateTime? CS_DeleteFlag { get; set; }
    }
}
