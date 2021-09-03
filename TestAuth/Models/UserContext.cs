using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Models
{
    public class UserContext: IdentityDbContext<User>
    {
        //public DbSet<User> Users { get; set; }
        public DbSet<ComputerParts> ComputerParts { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
