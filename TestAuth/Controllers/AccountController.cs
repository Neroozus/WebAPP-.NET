using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestAuth.Models;
using TestAuth.Services.Email;

namespace TestAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if(!await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        if (!await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError(string.Empty, "Подтвердите свой email");
                            return View(model);
                        }
                    }
                    
                }
                 var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("MainPage", "Home");
                    }
                

                }
                else
                {
                    ModelState.AddModelError("", "Неправильный Email и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
    
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = mapper.Map<User>(model);
                // добавляем пользователя
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
             
                    if(await sendEmailVerify(user) == true)
                    {
                        ConfirmEmailModel confirmEmailModel = mapper.Map<ConfirmEmailModel>(user);
                        await userManager.AddToRoleAsync(user, "User");                 
                        // установка куки
                        await signInManager.SignInAsync(user, false);
                        
                        return RedirectToAction("ConfirmEmail", confirmEmailModel);
                    }
                    else
                    {
                        return View("Error");
                    }

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<bool> sendEmailVerify(User user)
        {
            
            string token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;

            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userId = user.Id,
                token = token,
            }, protocol: HttpContext.Request.Scheme);
            VerifyEmail sendEmail = new VerifyEmail();
            if (await sendEmail.Send(tokenLink, user.Email) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            ConfirmEmailModel confirmEmailModel;
            var user = await userManager.FindByIdAsync(userId);
            
            if (user!=null)
            {
                confirmEmailModel = mapper.Map<ConfirmEmailModel>(user);
                if (user.EmailConfirmed == true)
                {
                    confirmEmailModel.emailVerified = true;
                    return View(confirmEmailModel);
                }
                if (string.IsNullOrEmpty(token) && confirmEmailModel.emailVerified == false)
                {
                    return View(confirmEmailModel);
                }
                if (!string.IsNullOrEmpty(token))
                {

                    try
                    {
                        var result = await userManager.ConfirmEmailAsync(user, token);
                        if (result.Succeeded)
                        {
                            confirmEmailModel.emailVerified = true;
                            return View(confirmEmailModel);
                        }

                    }
                    catch (System.Exception)
                    {
                        return View("Error");
                    }
                }
            }
               
            else
            {
                return View("Error");
                
            }


            return View(confirmEmailModel);
        }

       

        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await signInManager.SignOutAsync();
            return RedirectToAction("MainPage", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel confirmEmailModel)
        {
            var user = await userManager.FindByIdAsync(confirmEmailModel.userId);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    confirmEmailModel.isConfirmed = true;
                    return View(confirmEmailModel);
                }
            }
            else
            {
                return View("Error");
            }
            
            await sendEmailVerify(user);
            confirmEmailModel.emailSent=true;
            return View(confirmEmailModel);
        }

      /*  [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded) 
            {
                return RedirectToAction("MainPage", "Home");

            }
            else
            {
                return View("Error");
            }
                
        }*/

        /* public bool IsValidEmail(string email)
         {
             return new EmailAddressAttribute().IsValid(email);
         }*/
    }
}
