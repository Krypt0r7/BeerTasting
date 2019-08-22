using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string UserIdentity { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime SignedUp { get; private set; }
        public IList<UserTasting> Tastings { get; set; }
        public IList<Tasting> MeisterTastings { get; set; }
    }
}
