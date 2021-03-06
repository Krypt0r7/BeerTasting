﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTastingWebApp.Models
{
    public class Beer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public int SystemetNumber { get; set; }

    }
}
