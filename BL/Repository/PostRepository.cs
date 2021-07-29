using Freelance_System.BL.Interface;
using Freelance_System.DAL.Database;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using System;
using System.Linq;

namespace Freelance_System.BL.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContainer db;
        private readonly ICategoryRepository category;

        public PostRepository(DbContainer db,ICategoryRepository category)
        {
            this.db = db;
            this.category = category;
        }
        public void Add(PostVM post)
        {
            Post post1 = new Post()
            {
                Id = post.Id,
                Description = post.Description,
                CreationDate = DateTime.Now,
                Budget = post.Budget,
                ClientId = post.ClientId,
                CategoryId = post.CategoryId,                  
            };
            db.Post.Add(post1);
            db.SaveChanges();
        }

        public IQueryable<PostVM> GetAllPosts()
        {
            return db.Post.Select(p=>new PostVM { Id = p.Id,Budget=p.Budget,CategoryId=p.CategoryId,ClientId=p.ClientId,CreationDate=p.CreationDate,Description=p.Description, CategoryName = p.Categories.Name,ClientName=p.Client.UserName}) ;
        }
        public IQueryable<PostVM> GetClientPosts(string ClientId)
        {
            return db.Post.Where(p=>p.ClientId==ClientId).Select(p => new PostVM { Id = p.Id, Budget = p.Budget, CategoryId = p.CategoryId, ClientId = p.ClientId, CreationDate = p.CreationDate, Description = p.Description, CategoryName = p.Categories.Name, ClientName = p.Client.UserName });
        }

        public IQueryable<PostVM> GetNotAcceptedPosts()
        {
            return db.Post.Where(p => p.IsAccepted == false).Select(p => new PostVM { Id = p.Id, Budget = p.Budget, CategoryId = p.CategoryId, ClientId = p.ClientId, CreationDate = p.CreationDate, Description = p.Description, CategoryName = p.Categories.Name, ClientName = p.Client.UserName }); 
        }
        public IQueryable<PostVM> GetAcceptedPosts()
        {
            return db.Post.Where(p => p.IsAccepted == true).Select(p => new PostVM { Id = p.Id, Budget = p.Budget, CategoryId = p.CategoryId, ClientId = p.ClientId, CreationDate = p.CreationDate, Description = p.Description, CategoryName = p.Categories.Name, ClientName = p.Client.UserName }); 
        }

        public void AcceptPost(int id)
        {
            var post = GetPostById(id);
            post.IsAccepted = true;

            db.Entry(post).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }

        public void EditPost(PostVM post)
        {
            var OldPost = GetPostById(post.Id);
            OldPost.Budget = post.Budget;
            OldPost.Description = post.Description;
            OldPost.CategoryId = post.CategoryId;
            db.Entry(OldPost).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
            db.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = GetPostById(id);
            db.Post.Remove(post);
            db.SaveChanges();
        }

        public PostVM GetPost(int id)
        {
            var data = GetPostById(id);
            PostVM post = new PostVM();
            post.Id = data.Id;
            post.Description = data.Description;
            post.Budget = data.Budget;
            post.CreationDate = data.CreationDate;
            post.CategoryId = data.CategoryId;
            post.ClientId = data.ClientId;
            post.CategoryName = category.GetCategoryById(data.CategoryId).CategoryName;
            return post;
        }

        private Post GetPostById(int id)
        {
            return db.Post.Where(p => p.Id == id).FirstOrDefault();
        }



    }
}
