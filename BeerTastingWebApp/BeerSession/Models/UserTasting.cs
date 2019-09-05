using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Models
{
    public class UserTasting
    {
        public int TastingID { get; set; }
        public Tasting Tasting { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
