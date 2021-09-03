using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Interfaces;
using TestAuth.Models;

namespace TestAuth.Services.Repositories
{
    public class ComputerPartsRepo:IComputerPartsRepo
    {
        private UserContext context;
       // private UserManager<User> userManager;
        //private SignInManager<User> signInManager;
        public ComputerPartsRepo(UserContext context)
        {
            this.context = context;
        }
        public void Delete(ComputerParts computerPart)
        {
            context.ComputerParts.Remove(computerPart);
        }

        public List<ComputerParts> GetAllByUserEmail(string email)
        {
            List<ComputerParts> computerPart = context.ComputerParts.Where(c => c.User.Email == email).ToList();
            return computerPart;
        }

        public async Task<ComputerParts> GetById(int id)
        {
            var computerPart = await context.ComputerParts.Where(c => c.Id == id).FirstOrDefaultAsync();
            return computerPart;
        }

        public void Add(ComputerParts computerPart, User user)
        {
            context.ComputerParts.AddAsync(new ComputerParts
            {
                User = user,
                UserId = user.Id,
                Manufacturer = computerPart.Manufacturer,
                ComputerPart = computerPart.ComputerPart,
                Quantity = computerPart.Quantity

            });
        }

        public void Update(ComputerParts computerPartFirst, ComputerParts computerPartSecond)
        {
            computerPartFirst.Manufacturer = computerPartSecond.Manufacturer;
            computerPartFirst.ComputerPart = computerPartSecond.ComputerPart;
            computerPartFirst.Quantity = computerPartSecond.Quantity;
        }
    }
}
