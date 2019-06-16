using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace laba2.Models
{
    public class Class
    {
        [Required]
        public int Id { get; set; }
        public string ClassLead { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public int ClassTypeId { get; set; }
        public ClassType ClassType { get; set; }
        //public List<Student> Students { get; set; }
        //public List<Schedule> Schedules { get; set; }
    }
}
