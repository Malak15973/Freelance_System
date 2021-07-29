using Freelance_System.BL.Interface;
using Freelance_System.DAL.Database;
using Freelance_System.DAL.Entites;
using Freelance_System.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Freelance_System.BL.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly DbContainer db;
        public ProposalRepository(DbContainer db)
        {
            this.db = db;
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

            var client = db.Users.Where(u => u.Id == ClientId).Include(u => u.Posts).ThenInclude(p => p.Proposal).FirstOrDefault();
            foreach (var post in client.Posts)
            {
                foreach (var proposal in post.Proposal)
                {
                    result.Add(new ProposalVM { Id=proposal.Id,CreationDate= proposal.CreationDate,Description= proposal.Description,FreelancerId= proposal.FreelancerId,IsAccepted= proposal.IsAccepted,PostId= proposal.PostId});
                }
            }
            return result.AsQueryable();
        }

        public void AcceptProposal(int ProposalId)
        {
            var proposal = GetProposalById(ProposalId);
            proposal.IsAccepted = true;
            db.Entry(proposal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public void RefuseProposal(int ProposalId)
        {
            var proposal = GetProposalById(ProposalId);
            db.Proposal.Remove(proposal);
            db.SaveChanges();
        }

        public bool IsThisClientProposalAccepted(int ProposalId)
        {
            var proposal = GetProposalById(ProposalId);
            if (proposal.IsAccepted)
            {
                return true;
            }
            return false;
        }
        public IQueryable<PostVM> GetFreelancerAcceptedProposals(string FreelancerId)
        {
            var result = db.Proposal.
                Where(proposal => (proposal.FreelancerId == FreelancerId && proposal.IsAccepted == true)).
                Include(proposal => proposal.Post).
                ThenInclude(post => post.Client).
                Include(proposal => proposal.Post.Categories).
                Select(p=>new PostVM {Id=p.Post.Id,Budget= p.Post.Budget,Description= p.Post.Description,CreationDate= p.Post.CreationDate,ClientName= p.Post.Client.UserName,CategoryName= p.Post.Categories.Name});
           return result;
        }
        private Proposal GetProposalById(int ProposalId)
        {
            return db.Proposal.Where(p => p.Id == ProposalId).FirstOrDefault(); ;
        }
    }
}
