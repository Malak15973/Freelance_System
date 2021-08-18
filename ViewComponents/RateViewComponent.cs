using Freelance_System.BL.Interface;
using Freelance_System.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.ViewComponents
{
    public class RateViewComponent : ViewComponent
    {
        private readonly IRateRepository rateRepository;

        public RateViewComponent(IRateRepository rateRepository)
        {
            this.rateRepository = rateRepository;
        }
        public IViewComponentResult Invoke(string FreelancerId, int PostId)
        {
            var result = rateRepository.GetFreelancerRateForPost(FreelancerId, PostId);
            RateVM model = new RateVM
            {
                FreelancerId = FreelancerId,
                PostId = PostId,
                Rate = result
            };
            return View(model);
        }
    }
}
