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
        private DateTime? datecreated;
        public DateTime DateCreated
        {
            get { return datecreated ?? DateTime.Now; }
            set { datecreated = value; }
        }

        public ICollection<User> Participants { get; set; }
        public ICollection<Beer> Beers { get; set; }
        public User SessionMeister { get; set; }
    }
}
