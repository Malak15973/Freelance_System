using Freelance_System.Model;

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
