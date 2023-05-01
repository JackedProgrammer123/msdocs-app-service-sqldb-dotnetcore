using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreSqlDb.Models
{
    public class Property
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Cost { get; set; }

        [Display(Name = "Listed Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}

