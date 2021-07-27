using Freelance_System.BL.Interface;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Controllers
{
    [Authorize(Roles ="Client")]
    public class ClientController : Controller
    {
        private readonly IPostRepository post;
        private readonly ICategoryRepository category;
        private readonly IProposalRepository proposal;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientController(IPostRepository post,ICategoryRepository category,IProposalRepository proposal,UserManager<ApplicationUser> userManager)
        {
            this.post = post;
            this.category = category;
            this.proposal = proposal;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var posts = post.GetClientPosts(userManager.GetUserId(User));
            return View(posts);
        }
        
        public IActionResult CreateNewPost()
        {
            var categories = category.GetCategories();
            ViewBag.Categories = new SelectList(categories,"Id","CategoryName");
            ViewBag.ClientId = userManager.GetUserId(User);
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewPost(PostVM model)
        {
            if (ModelState.IsValid)
            {
                post.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult RecievedProposals()
        { 
            var ClientId = userManager.GetUserId(User);
            var result = proposal.GetClientProposals(ClientId);
            return View(result);
        }
        public IActionResult AcceptProposal(int ProposalId)
        {
            proposal.AcceptProposal(ProposalId);
            return RedirectToAction("RecievedProposals");
        }
        public IActionResult RefuseProposal(int ProposalId)
        {
            proposal.RefuseProposal(ProposalId);
            return RedirectToAction("RecievedProposals");
        }


    }
}
