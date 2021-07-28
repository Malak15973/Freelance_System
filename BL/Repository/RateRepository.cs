using Freelance_System.BL.Interface;
using Freelance_System.DAL.Database;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.BL.Repository
{
    public class RateRepository : IRateRepository
    {
        private readonly DbContainer db;

        public RateRepository(DbContainer db)
        {
            this.db = db;
        }
        public void Add(RateVM rate)
        {
            Rate userRate = new Rate()
            {
                FreelancerId = rate.FreelancerId,
                PostId = rate.PostId,
                FreelancerRate = rate.Rate
            };
            db.Rate.Add(userRate);
            db.SaveChanges();
        }

        public int GetFreelancerRateForPost(string FreelancerId, int PostId)
        {
            var result = db.Rate.Where(p => p.FreelancerId == FreelancerId && p.PostId == PostId).FirstOrDefault();
            if (result != null)
            {
                return result.FreelancerRate;
            }
            return 0;
        }

        public double GetPostTotalRate(int PostId)
        {
            double result = 0.0;
            var posts = db.Rate.Where(r => r.PostId == PostId);
            foreach (var post in posts)
            {
                result += post.FreelancerRate;
            }
            if (result != 0.0)
            {
                result /= posts.Count();
            }
            return result;
        }

        public void Update(RateVM rate)
        {
            var oldRate = db.Rate.Where(r => r.PostId == rate.PostId && r.FreelancerId == rate.FreelancerId).FirstOrDefault();
            oldRate.FreelancerRate = rate.Rate;
            db.Entry(oldRate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
