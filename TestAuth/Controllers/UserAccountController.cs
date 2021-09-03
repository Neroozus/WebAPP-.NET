using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestAuth.Models;

namespace TestAuth.Controllers
{
    public class UserAccountController : Controller
    {
        private UserContext db;
        public UserAccountController(UserContext context)
        {
            db = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            if (user.Info != null)
            {
                return View("Account", user);
            }
            else
            {
                user.Info = "Тут пока пусто";
                return View("Account", user);
            }
            
        }
        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> EditSettings(AccountSettingsModel settingsModel)
        {
            using (db)
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    user.Info = settingsModel.Info;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return Content("Не нашелся");
            }

        }

    }
}
