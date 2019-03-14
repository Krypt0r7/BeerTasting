﻿using System;
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
        public string Password { get; set; }
        public DateTime? DateCreated { get; private set; }

        public ICollection<Participant> Partisipants { get; set; }
        public ICollection<UserTasting> Users { get; set; }
        public ICollection<Beer> Beers { get; set; }
        public User SessionMeister { get; set; }
    }
}
