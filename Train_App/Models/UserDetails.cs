using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_App.Models
{
    public class UserDetails
    {
        public static long UNumber { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public long Number { get; set; }
        public string Password { get; set; }
        public decimal Wallet { get; set; }
    }
}
