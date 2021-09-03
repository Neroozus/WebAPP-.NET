using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Models;

namespace TestAuth.Interfaces
{
    public interface IComputerPartsRepo
    {
        public List<ComputerParts> GetAllByUserEmail(string email);
        public Task<ComputerParts> GetById(int id);
        public void Add(ComputerParts computerPart, User user);
        public void Update(ComputerParts computerPartFirst, ComputerParts computerPartsSecond);
        public void Delete(ComputerParts computerPart);
        
    }
}
