using Freelance_System.BL.Helpers;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO; 
using System.Threading.Tasks;

namespace Freelance_System.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        } 
        [Authorize(Roles = "Admin,Client")]

        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            ProfileVM profile = new ProfileVM()
            {
                UserName = user.UserName,
                Email = user.Email,
                Number = user.PhoneNumber,
                PhotoName = user.PhotoPath

            };
            return View(profile);
        }
        [Authorize(Roles = "Admin,Client")]

        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User); 
            ProfileVM profile = new ProfileVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Number = user.PhoneNumber ,
                PhotoName = user.PhotoPath 
            };
            return View(profile);
        }
        [Authorize(Roles = "Admin,Client")]

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileVM profile)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(profile.Id);

                if (profile.Photo != null)
                {
                    if (user.PhotoPath != null)
                    {
                        RemoveFilesHelper.RemoveFile(user.PhotoPath);
                    }
                    string PhotoPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos";
                    string PhotoName = Guid.NewGuid() + Path.GetFileName(profile.Photo.FileName);
                    string FinalPath = Path.Combine(PhotoPath, PhotoName);
                    user.PhotoPath = PhotoName; 
                    using (var Stream = new FileStream(FinalPath, FileMode.Create))
                    {
                        profile.Photo.CopyTo(Stream);
                    } 
                }

                user.PhoneNumber = profile.Number;
                user.UserName = profile.UserName;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {  
                   return RedirectToAction("Profile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(profile);
        }

        public async Task<JsonResult> IsUserNameInUse(string UserName)
        {
            var result = await userManager.FindByNameAsync(UserName); 
            if (result == null||User.Identity.Name == UserName)
            {
                return Json(true);
            }
            else
            {
                return Json($"Name {UserName} Is In Use");
            }
        }
    }
}
