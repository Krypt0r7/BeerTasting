using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Models
{
    public class TastingRoom
    {
        [Key]
        public int ID { get; set; }
        public string RoomName { get; set; }
        public Tasting Tasting { get; set; }
        public int TastingId { get; set; }
    }
}
