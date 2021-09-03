using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Models;
using TestAuth.ViewModels;
using TestAuth.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;

namespace TestAuth.Controllers
{
    [Authorize]
        
    public class HomeController : Controller
    {

        private IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public HomeController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult UpdateTable(string message, bool isSuccess)
        {
            List<ComputerParts> computerParts = unitOfWork.ComputerPartsRepo.GetAllByUserEmail(User.Identity.Name);
            ViewComputerPartsModel viewComputerPartsModel = new() { computerPartsCollection = computerParts };
            if (isSuccess == true)
            {
                TempData["messageSuccess"] = message;
            }
            else
            {
               TempData["messageDanger"] = message;
            }
            return PartialView("PartialView/ComputerPartsPartial", viewComputerPartsModel);
        }
        [HttpGet]
        [Authorize]
        public IActionResult MainPage()
        {
            List<ComputerParts> computerParts = unitOfWork.ComputerPartsRepo.GetAllByUserEmail(User.Identity.Name);
            ViewComputerPartsModel viewComputerPartsModel = new() { computerPartsCollection = computerParts };
            return View(viewComputerPartsModel);

        }


        [HttpGet]
        public IActionResult AddPart()
        {
            try
            {
                ComputerPartsModel computerPartsModel = new ComputerPartsModel();
                return PartialView("PartialModal/AddPartPartial", computerPartsModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPart(ComputerPartsModel computerPartsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
                    ComputerParts computerParts = mapper.Map<ComputerParts>(computerPartsModel);
                    unitOfWork.ComputerPartsRepo.Add(computerParts, user);
                    await unitOfWork.Save();

                    return RedirectToAction("MainPage", "Home");

                }
                else
                {
                    return PartialView("PartialModal/AddPartPartial", computerPartsModel);
                }
            }
            catch (Exception)
            {
                return NotFound();

            }

        }

        [HttpGet]
        public async Task<IActionResult> DeletePart(int id)
        {
            var part = await unitOfWork.ComputerPartsRepo.GetById(id);
            ComputerPartsModel modelPart = mapper.Map<ComputerPartsModel>(part);
            if (part != null)
            {
                return PartialView("PartialModal/DeletePartPartial", modelPart);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeletePart(ComputerParts computerParts)
        {
            try
            {
                unitOfWork.ComputerPartsRepo.Delete(computerParts);
                await unitOfWork.Save();
                return RedirectToAction("MainPage", "Home");
            }
            catch(Exception)
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        public async Task <IActionResult> EditPart(int id)
        {
            var part = await unitOfWork.ComputerPartsRepo.GetById(id);
            ComputerPartsModel modelPart = mapper.Map<ComputerPartsModel>(part);

            if (part != null)
            {
                return PartialView("PartialModal/EditPartPartial", modelPart);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPart(ComputerPartsModel computerPartsModel)
        {
            if (ModelState.IsValid)
            {
                var computerPartOriginal = await unitOfWork.ComputerPartsRepo.GetById(computerPartsModel.Id);
                if (computerPartOriginal != null)
                {
                    ComputerParts computerPartEdit = mapper.Map<ComputerParts>(computerPartsModel);
                    unitOfWork.ComputerPartsRepo.Update(computerPartOriginal,computerPartEdit);
                    await unitOfWork.Save();

                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return PartialView("PartialModal/EditPartPartial", computerPartsModel);
            }
            return RedirectToAction("MainPage", "Home");


        }
    }
    
}
