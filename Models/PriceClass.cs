using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba2.Models
{
    public class PriceClass
    {
        public PriceClass() {}

        public PriceClass(string classType, string classLead, int count, DateTime date)
        {
            ClassType = classType;
            ClassLead = classLead;
            Count = count;
            Date = date;
        }
        public string ClassType { get; set; }
        public string ClassLead { get; set; } 
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public int ClassTypeId { get; set; }
    }
}
