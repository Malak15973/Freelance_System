using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using System.Linq;

namespace Freelance_System.BL.Interface
{
    public interface IPostRepository
    {
        public void Add(PostVM post);
        public IQueryable<PostVM> GetAllPosts();
        public PostVM GetPost(int id);
        public IQueryable<PostVM> GetClientPosts(string ClientId);
        public IQueryable<PostVM> GetNotAcceptedPosts();
        public IQueryable<PostVM> GetAcceptedPosts();
        public void AcceptPost(int id);
        public void EditPost(PostVM post);
        public void DeletePost(int id);
    }
}
