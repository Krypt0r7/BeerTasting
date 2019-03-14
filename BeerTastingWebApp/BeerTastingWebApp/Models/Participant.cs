﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTastingWebApp.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Tasting Tasting { get; set; }

    }
}
