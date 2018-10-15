using System;
using SQLite;

namespace Q.Models
{
    public class Tbl_user
    {
        [PrimaryKey]
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
    }
}
