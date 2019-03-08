using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTastingWebApp.Models
{
    public class Tasting
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set;}
        public DateTime DateCreated { get; private set; }

        public IList<UserTasting> Participants { get; set; }
        public ICollection<Beer> Beers { get; set; }
        public User SessionMeister { get; set; }
    }
}
