using Freelance_System.BL.Interface;
using Freelance_System.DAL.Database;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.BL.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly DbContainer db;
        private readonly IPostRepository postRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoryRepository categoryRepository;

        public ProposalRepository(DbContainer db,IPostRepository postRepo,UserManager<ApplicationUser> userManager,ICategoryRepository categoryRepository)
        {
            this.db = db;
            this.postRepo = postRepo;
            this.userManager = userManager;
            this.categoryRepository = categoryRepository;
        }
        public void ApplyForJob(ProposalVM proposal)
        {
            Proposal prop = new Proposal()
            {
                FreelancerId = proposal.FreelancerId,
                Description = proposal.Description,
                CreationDate = DateTime.Now,
                PostId = proposal.PostId,
                IsAccepted = false
            };
            db.Proposal.Add(prop);
            db.SaveChanges();
        }
        public bool IsAlreadyApplied(int PostId, string FreelancerId)
        {
            var job = db.Proposal.Where(p => p.FreelancerId == FreelancerId && p.PostId == PostId).FirstOrDefault();
            if (job == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IQueryable<ProposalVM> GetClientProposals(string ClientId)
        {
            List<ProposalVM> result = new List<ProposalVM>();
            var posts = postRepo.GetClientPosts(ClientId);
            var proposals = db.Proposal.Select(p => new ProposalVM { Id = p.Id, CreationDate = p.CreationDate, Description = p.Description, FreelancerId = p.FreelancerId, IsAccepted = p.IsAccepted, PostId = p.PostId });
            foreach (var proposal in proposals)
            {
                foreach (var post in posts)
                {
                    if (proposal.PostId == post.Id)
                    {
                        result.Add(proposal);
                    }
                }
            }
            return result.AsQueryable();
        }

        public void AcceptProposal(int ProposalId)
        {
            var proposal = db.Proposal.Where(p => p.Id == ProposalId).FirstOrDefault();
            proposal.IsAccepted = true;
            db.Entry(proposal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public void RefuseProposal(int ProposalId)
        {
            var proposal = db.Proposal.Where(p => p.Id == ProposalId).FirstOrDefault();
            db.Proposal.Remove(proposal);
            db.SaveChanges();
        }

        public bool IsThisClientProposalAccepted(int ProposalId)
        {
            var proposal = db.Proposal.Where(p => p.Id == ProposalId).FirstOrDefault();
            if (proposal.IsAccepted)
            {
                return true;
            }
            return false;
        }

        public async Task<IQueryable<PostVM>> GetFreelancerAcceptedProposals(string FreelancerId) 
        {
            List<PostVM> result = new List<PostVM>();
            var AcceptedProposals = db.Proposal.Where(p => (p.FreelancerId == FreelancerId && p.IsAccepted == true)).Include(p=>p.Post);
            foreach (var AcceptedProposal in AcceptedProposals)
            {
                var client = await userManager.FindByIdAsync(AcceptedProposal.Post.ClientId);
                var ClientName = client.UserName;
                var Category = categoryRepository.GetCategoryById(AcceptedProposal.Post.CategoryId);
                result.Add(new PostVM {Id=AcceptedProposal.Post.Id , Budget = AcceptedProposal.Post.Budget,Description= AcceptedProposal.Post.Description,CategoryName=Category.CategoryName,ClientName=ClientName,CreationDate= AcceptedProposal.Post.CreationDate });
            }
            return result.AsQueryable();
        }
    }
}
