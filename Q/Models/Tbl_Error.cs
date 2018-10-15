using System;
using SQLite;

namespace Q.Models
{
    public class Tbl_Error
    {
        [PrimaryKey]
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
    }
}
