using Freelance_System.BL.Interface;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_System.Controllers
{
    public class FreelancerController : Controller
    {
        private readonly IPostRepository post;
        private readonly IProposalRepository proposal;
        private readonly ISavedPostsRepository savedPostsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRateRepository rateRepository;

        public FreelancerController(IPostRepository post,IProposalRepository proposal,ISavedPostsRepository savedPostsRepository,UserManager<ApplicationUser> userManager, IRateRepository rateRepository)
        {
            this.post = post;
            this.proposal = proposal;
            this.savedPostsRepository = savedPostsRepository;
            this.userManager = userManager;
            this.rateRepository = rateRepository;
        }
        public IActionResult Index()
        {
            var FreelancerId  = userManager.GetUserId(User);
            ViewBag.FreelancerId = FreelancerId;
            var data = post.GetAcceptedPosts();
            foreach (var post in data)
            {
                ViewData[$"{post.Id}"] = rateRepository.GetFreelancerRateForPost(FreelancerId, post.Id);
            } 
            return View(data);
        }
        public IActionResult SavePost(int id)
        {
            SavePostVM savePost = new SavePostVM()
            {
                PostId = id,
                FreelancerId = userManager.GetUserId(User),
            } ;
            savedPostsRepository.SavePost(savePost);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteSavedPost(int id)
        {
            SavePostVM savePost = new SavePostVM()
            {
                PostId = id,
                FreelancerId = userManager.GetUserId(User),
            };
            savedPostsRepository.DeleteSavedPost(savePost);
            return RedirectToAction("SavedPosts");
        }
        public IActionResult ApplyForJob(int id)
        {
            ViewBag.PostDescription = post.GetPost(id).Description;
            ViewBag.PostId = id;
            ViewBag.FreelancerId = userManager.GetUserId(User);
            return View();
        }
        [HttpPost]
        public IActionResult ApplyForJob(ProposalVM model)
        {
            if (ModelState.IsValid)
            {
                proposal.ApplyForJob(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult SavedPosts()
        {
            var FreelancerId = userManager.GetUserId(User);
            ViewBag.FreelancerId = FreelancerId;
            var data = savedPostsRepository.GetFreelancerSavedPosts(FreelancerId); 
            return View(data);
        }
        public IActionResult AcceptedProposals()
        {
            var FreelancerId = userManager.GetUserId(User);
            var posts = proposal.GetFreelancerAcceptedProposals(FreelancerId);
            return View(posts);
        }
        [HttpPost]
        public JsonResult RatePost(RateVM model)
        {
            rateRepository.Add(model);
            var totalRate = rateRepository.GetPostTotalRate(model.PostId);
            return Json(totalRate);
        }
        [HttpPost]
        public JsonResult UpdateRate(RateVM model)
        {
            rateRepository.Update(model);
            var totalRate = rateRepository.GetPostTotalRate(model.PostId);
            return Json(totalRate);
        }

    }
}
