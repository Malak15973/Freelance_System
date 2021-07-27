using Freelance_System.Model;
using System.Linq;

namespace Freelance_System.BL.Interface
{
    public interface ISavedPostsRepository
    {
        public void SavePost(SavePostVM savePost);
        public void DeleteSavedPost(SavePostVM savePost);
        public IQueryable<PostVM> GetFreelancerSavedPosts(string FreelancerId);
        public bool IsFreelancerSavePost(int PostId,string FreelancerId);
    }
}
