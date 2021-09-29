using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Models
{
    public class Fan
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Wallet Wallet { get; set; } = new Wallet();
        public List<Creator> Creators { get; set; } = new List<Creator>();
    }
}
