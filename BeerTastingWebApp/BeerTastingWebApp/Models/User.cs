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
        [Required(ErrorMessage = "Have to enter a username")]
        public string UserName { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime SignedUp { get; private set; }
        public IList<UserTasting> Tastings { get; set; }
    }
}
