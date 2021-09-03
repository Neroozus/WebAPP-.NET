using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Interfaces;
using TestAuth.Services.Repositories;
using TestAuth.Models;

namespace TestAuth.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserContext context;
        private IComputerPartsRepo computerPartsRepo;

        public UnitOfWork(UserContext context)
        {
            this.context = context;
        }
        public IComputerPartsRepo ComputerPartsRepo
        {
            get
            {
                return computerPartsRepo = computerPartsRepo ?? new ComputerPartsRepo(context);
            }
        }
        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
