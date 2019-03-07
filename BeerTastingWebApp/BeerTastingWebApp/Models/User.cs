using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTastingWebApp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        private DateTime? signedUp;
        public DateTime SignedUp
        {
            get { return signedUp ?? DateTime.Now; }
            set { signedUp = value; }
        }
        public ICollection<Tasting> Tastings { get; set; }
    }
}
