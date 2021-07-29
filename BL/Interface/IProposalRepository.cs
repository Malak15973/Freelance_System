using Freelance_System.Model;
using System.Linq;

namespace Freelance_System.BL.Interface
{
    public interface IProposalRepository
    {
        public void ApplyForJob(ProposalVM proposal);
        public bool IsAlreadyApplied(int PostId, string FreelancerId);
        public IQueryable<ProposalVM> GetClientProposals(string ClientId);
        public void AcceptProposal(int ProposalId);
        public void RefuseProposal(int ProposalId);
        public bool IsThisClientProposalAccepted(int ProposalId);
        public IQueryable<PostVM> GetFreelancerAcceptedProposals(string FreelancerId);

    }
}
