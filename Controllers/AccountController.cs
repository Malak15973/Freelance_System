using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }
        public IActionResult Login()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Profile", "Home");
            }
            else if(User.IsInRole("Client"))
            {
                return RedirectToAction("Index", "Client");
            }
            else if (User.IsInRole("Freelancer"))
            {
                return RedirectToAction("Index", "Freelancer");
            }
            return View();
        }
        
        [HttpPost] 
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName); 
                if (user != null && !user.EmailConfirmed &&
                                    (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RemeberMe, false);
                if (result.Succeeded)
                {
                    var Role = await userManager.GetRolesAsync(user);
                    if(Role.FirstOrDefault()== "Admin")
                    {
                        return RedirectToAction("Profile", "Home"); 
                    }else if(Role.FirstOrDefault() == "Client")
                    {
                        return RedirectToAction("Index", "Client"); 
                    }else if (Role.FirstOrDefault() == "Freelancer")
                    {
                        return RedirectToAction("Index", "Freelancer");
                    }
                }
                else
                { 
                    ModelState.AddModelError("", "Invalid UserName Or Password");      
                }
            }
            return View(model);
        }
        
        public IActionResult Register()
        {
            var roles = roleManager.Roles;
            ViewBag.RolesList = new SelectList(roles, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            { 
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                if(await userManager.FindByEmailAsync(model.Email) == null)
                {
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(model.RoleId);
                        await userManager.AddToRoleAsync(user, role.Name);
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var ConfirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                        logger.Log(LogLevel.Warning, ConfirmationLink);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Email {model.Email} Is Already In Use");
                }

            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ConfirmEmailAsync(user, token);
            if(result == null)
            {
                ViewBag.Error = "Email Cannot be confirmed";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, passwordResetLink);
                    return RedirectToAction("ConfirmForgetPassword");
                }
                return RedirectToAction("ConfirmForgetPassword");
            }
            return View(model);
        }

        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email,string Token)
        {
            if (Email == null || Token == null)
            {
               ModelState.AddModelError("", "Invalid Data");      
            }
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return RedirectToAction("Login"); 
            }
            return View(model);
        }
        
        public async Task<JsonResult> IsUserNameInUse(string UserName)
        {
            var result = await userManager.FindByNameAsync(UserName);
            if (result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Name {UserName} Is In Use");
            }
        }
        public async Task<JsonResult> IsEmailInUse(string Email)
        {
            var result = await userManager.FindByEmailAsync(Email);
            if (result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} Is In Use");
            }
        }
    }
}
