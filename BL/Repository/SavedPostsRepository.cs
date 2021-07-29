using Freelance_System.BL.Interface;
using Freelance_System.DAL.Database;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Freelance_System.BL.Repository
{
    public class SavedPostsRepository : ISavedPostsRepository
    {
        private readonly DbContainer db;

        public SavedPostsRepository(DbContainer db)
        {
            this.db = db;
        }
        public void SavePost(SavePostVM savePost)
        {
            SavedPosts post = new SavedPosts()
            {
                PostId = savePost.PostId,
                FreelancerId = savePost.FreelancerId,
            };
            db.SavedPosts.Add(post);
            db.SaveChanges();
        }

        public void DeleteSavedPost(SavePostVM savePost)
        {
            var post = db.SavedPosts.Where(p => (p.FreelancerId == savePost.FreelancerId && p.PostId == savePost.PostId)).FirstOrDefault();
            db.SavedPosts.Remove(post);
            db.SaveChanges();
        }

        public IQueryable<PostVM> GetFreelancerSavedPosts(string FreelancerId)
        {

            var result = db.SavedPosts.Where(p => p.FreelancerId == FreelancerId).Include(p => p.Post).
                Select(savedPost => new PostVM { Id = savedPost.Post.Id, Budget = savedPost.Post.Budget, Description = savedPost.Post.Description, CreationDate = savedPost.Post.CreationDate, ClientId = savedPost.Post.ClientId, CategoryId = savedPost.Post.CategoryId });
            return result;
        }

        public bool IsFreelancerSavePost(int PostId, string FreelancerId)
        {
            var result = db.SavedPosts.Where(p => p.PostId == PostId && p.FreelancerId == FreelancerId).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}