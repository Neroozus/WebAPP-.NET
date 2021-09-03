using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Models
{
    public class ComputerParts
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string ComputerPart { get; set; }

        public int Quantity { get; set; }
    }
}
