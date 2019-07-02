using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Models
{
    public class Beer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public decimal Alchohol { get; set; }
        public int SystemetNumber { get; set; }
        public string Image { get; set; }
        public Tasting Tasting { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
