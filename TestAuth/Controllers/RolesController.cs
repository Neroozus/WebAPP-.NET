using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestAuth.Models;
using Microsoft.AspNetCore.Authorization;

namespace TestAuth.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index() 
        {
            return View(roleManager.Roles.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {

            if (!string.IsNullOrEmpty(name))
            {
                IdentityRole nameRole = await roleManager.FindByNameAsync(name);
                if (nameRole == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        TempData["messageSuccess"] = "Роль " + name + " добавлена.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    TempData["messageDanger"] = "Невозможно добавить, так как роль " + name + " уже существует.";
                    return RedirectToAction("Index");
                }
            }

            return View(name);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                return PartialView("PartialModal/DeletePartPartial", role);
            }
            else
            {
                return View("Account/Error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole roleFailure)
        {
            IdentityRole role2 = await roleManager.FindByIdAsync(roleFailure.Id);
            IdentityResult result = await roleManager.DeleteAsync(role2);
            if (result.Succeeded)
            {
                TempData["messageSuccess"] = "Роль " + role2.Name + " удалена.";
            }
            else
            {
                TempData["messageDanger"] = "Роль " + role2.Name + " не существует.";
            }
            return RedirectToAction("Index");

        }


        public IActionResult UserList() 
        {
            return View(userManager.Users.ToList());
        } 

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleModel model = new ChangeRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получаем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);

                await userManager.RemoveFromRolesAsync(user, removedRoles);
                TempData["messageSuccess"] = "Роли пользователя изменены.";
            }
            else
            {
                TempData["messageDanger"] = "Роли пользователя не были изменены.";

            }
           return RedirectToAction("UserList");

        }
    }
}

