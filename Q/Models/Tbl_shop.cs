using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Q.Models
{
    public class Tbl_shop
    {
        [PrimaryKey]
        public string Shop_ID { get; set; }
        public string US_ID { get; set; }
        public string Shop_Name { get; set; }
        public string Shop_Adress { get; set; }
        public DateTime? Shop_DateVisit { get; set; }
        public double? Shop_Latitude { get; set; }
        public double? Shop_Longitude { get; set; }
        public string Shop_PhoneNumber { get; set; }
        public DateTime? Shop_SyncFlag { get; set; }
        public DateTime? Shop_DeleteFlag { get; set; }
    }
}
