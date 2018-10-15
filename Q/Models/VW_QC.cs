using System;
using SQLite;
namespace Q.Models
{
    public class VW_QC
    {
        public string US_ID { get; set; }
        public string US_Name { get; set; }
        public string US_Username { get; set; }
        public string US_PassWord { get; set; }
        public string US_Detail { get; set; }
        public DateTime? US_StartDate { get; set; }
        public string US_Role { get; set; }
        public string US_Level { get; set; }
        public string US_Status { get; set; }
        public DateTime? US_SyncFlag { get; set; }
        public DateTime? US_DeleteFlag { get; set; }
        public string ER_ID { get; set; }
        public string ER_Name { get; set; }
        public string ER_Parameter { get; set; }
        public string ER_Property { get; set; }
        public string ER_Issue { get; set; }
        public string ER_NhomLoi { get; set; }
        public string ER_LoaiLoi { get; set; }
        public string ER_ChiTietLoi { get; set; }
        public string ER_Image { get; set; }
        public bool? ER_Status { get; set; }
        public string ER_Role { get; set; }
        public DateTime? ER_SyncFlag { get; set; }
        public DateTime? ER_DeleteFlag { get; set; }
        public double SKU_Barcode { get; set; }
        public string SKU_Name { get; set; }
        public string SKU_Detail { get; set; }
        public string SKU_Role { get; set; }
        public DateTime? SKU_SyncFlag { get; set; }
        public DateTime? SKU_DeleteFlag { get; set; }
        public string Shop_ID { get; set; }
        public string Shop_Name { get; set; }
        public string Shop_Adress { get; set; }
        public DateTime? Shop_DateVisit { get; set; }
        public double? Shop_Latitude { get; set; }
        public double? Shop_Longitude { get; set; }
        public string Shop_PhoneNumber { get; set; }
        public DateTime? Shop_SyncFlag { get; set; }
        public DateTime? Shop_DeleteFlag { get; set; }
        public string CS_Warning { get; set; }
        public string CS_ExpiryProduction { get; set; }
        public string CS_ExpiryProductionDate { get; set; }
        public string CS_Image { get; set; }
        public DateTime? CS_SyncFlag { get; set; }
        public DateTime? CS_DeleteFlag { get; set; }
    }
}
