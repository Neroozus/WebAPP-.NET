using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Models
{
    public class ConfirmEmailModel
    {
        public string userId { get; set; }
        public bool emailVerified { get; set; }
        public bool emailSent { get; set; }
        public bool isConfirmed { get; set; }
    }
}
