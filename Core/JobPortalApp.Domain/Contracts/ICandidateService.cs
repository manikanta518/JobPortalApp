using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortalApp.Model;

namespace JobPortalApp.Domain.Contracts
{
    public interface ICandidateService
    {
        /// <summary>
        /// Gets the candidates.
        /// </summary>
        /// <returns></returns>
        Task<List<Candidate>> GetCandidates();

        /// <summary>
        /// Adds the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns></returns>
        Task Add(Candidate candidate);

    }
}