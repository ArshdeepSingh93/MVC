using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class Log
    {
        [Key]
        public Guid LogID { get; set; }

        
        public string EventType { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string RecordID { get; set; }

        [Required]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }

        [Required]
        public DateTime Created_date { get; set; }
    }
}