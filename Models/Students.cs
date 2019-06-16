using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace laba2.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
