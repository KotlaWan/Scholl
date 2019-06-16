using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace laba2.Models
{
    public class Subject
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}

