using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Services.Email
{
    public interface IEmailService
    {
        public Task<bool> Send(string tokenLink, string email);
    }
}
