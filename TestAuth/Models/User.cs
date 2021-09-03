using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Models
{
    public class User: IdentityUser
    {
        public string Info { get; set; }

        public List<ComputerParts> computerParts { get; set; }
    }
}
