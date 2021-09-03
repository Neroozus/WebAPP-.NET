using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Models;
using TestAuth.ViewModels;
using System.Threading;

namespace TestAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private UserContext db;
        public HomeController(UserContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MainPage()
        {
            List<ComputerParts> computerParts = await db.ComputerParts
                .Where(p => p.User.Email == User.Identity.Name)
                .ToListAsync();
            ViewComputerPartsModel viewComputerPartsModel = new ViewComputerPartsModel { computerPartsCollection = computerParts };
            return View(viewComputerPartsModel);

        }


    //    [HttpGet]
      //  public IActionResult AddPart()
       // {
         //   ComputerPartsModel computerPartsModel = new ComputerPartsModel();
           // return PartialView("PartialModal/AddPartPartial", computerPartsModel);
        //}
        [HttpPost]
        public async Task<IActionResult> AddPart(ComputerPartsModel computerPartsModel)
        {
            if (ModelState.IsValid)
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
                db.ComputerParts.Add(new ComputerParts
                {
                    UserId = user.Id,
                    Manufacturer = computerPartsModel.Manufacturer,
                    ComputerPart = computerPartsModel.ComputerPart,
                    Quantity = (int)computerPartsModel.Quantity
                    
                });
                await db.SaveChangesAsync();

                
            }
            return PartialView("PartialModal/AddPartPartial", computerPartsModel);

        }

        [HttpGet]
        public async Task<IActionResult> DeletePart(int id)
        {
            var part = await db.ComputerParts.FirstOrDefaultAsync(p => p.Id == id);
            return PartialView("PartialModal/DeletePartPartial", part);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePart(ComputerParts computerParts)
        {
            var part = await db.ComputerParts.FirstOrDefaultAsync(p => p.Id == computerParts.Id);
            if (part != null)
            {
                db.ComputerParts.Remove(part);
                await db.SaveChangesAsync();
                return RedirectToAction("MainPage", "Home");
            }
            return RedirectToAction("MainPage", "Home");
        }

        [HttpGet]
        public async Task <IActionResult> EditPart(int? id)
        {
            var part = await db.ComputerParts.FirstOrDefaultAsync(p => p.Id == id);
            ComputerPartsModel computerPartsModel = new ComputerPartsModel();
            computerPartsModel.ComputerPart = part.ComputerPart;
            computerPartsModel.Manufacturer = part.Manufacturer;
            computerPartsModel.Quantity = part.Quantity;
            computerPartsModel.Id = part.Id;
            return PartialView("PartialModal/EditPartPartial", computerPartsModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPart(ComputerPartsModel computerParts)
        {
            if (ModelState.IsValid)
            {
                var part = await db.ComputerParts.FirstOrDefaultAsync(p => p.Id == computerParts.Id);
                if (part != null)
                {
                    part.Manufacturer = computerParts.Manufacturer;
                    part.Quantity = (int)computerParts.Quantity;
                    part.ComputerPart = computerParts.ComputerPart;
                    await db.SaveChangesAsync();
                }
            }

                return PartialView("PartialModal/EditPartPartial", computerParts);

            
           // return RedirectToAction("MainPage", "Home");



        }

    }
}
