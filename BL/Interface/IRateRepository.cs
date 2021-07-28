using Freelance_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.BL.Interface
{
    public interface IRateRepository
    {
        public void Add(RateVM rate);
        public void Update(RateVM rate);
        public double GetPostTotalRate(int PostId);
        public int GetFreelancerRateForPost(string FreelancerId, int PostId);


    }
}
