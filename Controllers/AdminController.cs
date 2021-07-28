using Freelance_System.BL.Helpers;
using Freelance_System.BL.Interface;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostRepository post;
        private readonly ICategoryRepository category;

        public AdminController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,IPostRepository post,ICategoryRepository category)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.post = post;
            this.category = category;
        }
        public IActionResult CreateRole()
        {
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole() {
                   Name = model.RoleName  
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("CreateRole");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ManageUsers()
        {
            
            var data = userManager.Users ;
            List<string> res = new List<string>();
            foreach (var user in data)
            {
                var Role = await userManager.GetRolesAsync(user);
                string RoleName = Role.FirstOrDefault();
                res.Add(RoleName);
            }
            ViewBag.Roles = res.ToArray();
            var users = data.Select(u => new ManageUserVM { Id = u.Id, UserName = u.UserName, Email = u.Email, Number = u.PhoneNumber });

            return View(users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var Role = await userManager.GetRolesAsync(user);
            var r = await roleManager.FindByNameAsync(Role.FirstOrDefault());
            ManageUserVM manageUserVM = new ManageUserVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Number = user.PhoneNumber,
                RoleName = r.Id
            };
            ViewBag.RolesList = new SelectList(roleManager.Roles, "Id", "Name") ;
            return View(manageUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManageUserVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(model.Id);
                user.UserName = model.UserName;
                user.PhoneNumber = model.Number;
                var NewRole = await roleManager.FindByIdAsync(model.RoleName);
                var OldRole = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRoleAsync(user, OldRole.FirstOrDefault());
                await userManager.AddToRoleAsync(user, NewRole.Name);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ManageUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description); 
                    }
                }
            }
            
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var Role = await userManager.GetRolesAsync(user);
            ManageUserVM manageUserVM = new ManageUserVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Number = user.PhoneNumber,
                RoleName = Role.FirstOrDefault()
            };
            return View(manageUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ManageUserVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(model.Id);
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    RemoveFilesHelper.RemoveFile(user.PhotoPath);
                    return RedirectToAction("ManageUsers");
                } 
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }


        public IActionResult PostsPage()
        {
            var data = post.GetAcceptedPosts();
            return View(data);
        }
        public IActionResult PostsRequests()
        {
            var data = post.GetNotAcceptedPosts();
            return View(data);
        }
        [HttpPost]
        public IActionResult AcceptPost(int id)
        {
            post.AcceptPost(id);
            return RedirectToAction("PostsRequests");
        }

        public IActionResult EditPost(int id)
        {
            var data = post.GetPost(id);
            var categories = category.GetCategories();
            ViewBag.CategoriesList = new SelectList(categories, "Id", "CategoryName"); 
            return View(data);
        }
        [HttpPost]
        public IActionResult EditPost(PostVM NewPost)
        {
            if (ModelState.IsValid)
            {
                post.EditPost(NewPost);
                return RedirectToAction("PostsRequests");

            }
            return View(NewPost);
        }
        public IActionResult DeletePost(int id)
        {
            var data = post.GetPost(id); 
            return View(data);
        }
        [HttpPost]
        public IActionResult DeletePost(PostVM DeletedPost)
        {
            if (ModelState.IsValid)
            {
                post.DeletePost(DeletedPost.Id);
                return RedirectToAction("PostsRequests"); 
            }
            return View(DeletedPost);
        }








        //---------------------------------------Ajax-------------------------------------------




        [HttpPost]
        public async Task<JsonResult> GetUsersBySearch(string searchKey)
        {
            var users = userManager.Users; 
            List<ManageUserVM> result = new List<ManageUserVM>();
            foreach (var user in users)
            {
                var Role = await userManager.GetRolesAsync(user);
                if (searchKey == null || searchKey == "") { 
                    result.Add(new ManageUserVM { Id = user.Id, UserName = user.UserName, Email = user.Email, Number = user.PhoneNumber, RoleName = Role.FirstOrDefault() }); 
                } 
                else if (user.UserName.Contains(searchKey) || user.Email.Contains(searchKey) || user.PhoneNumber.Contains(searchKey) || Role.FirstOrDefault().Contains(searchKey))
                {
                    result.Add(new ManageUserVM { Id = user.Id, UserName = user.UserName, Email = user.Email, Number = user.PhoneNumber, RoleName = Role.FirstOrDefault() });
                }
            }
            return Json(result);
        }
    }
}
