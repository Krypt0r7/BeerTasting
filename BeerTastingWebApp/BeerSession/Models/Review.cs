using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int Taste { get; set; }
        public int Smell { get; set; }
        public int Look { get; set; }
        public int OverallRating { get; set; }
        public decimal Score { get; set; }
        public string Description { get; set; }
        public Beer Beer { get; set; }
        public User User { get; set; }
    }
}

