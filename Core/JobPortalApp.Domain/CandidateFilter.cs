using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortalApp.Domain.Contracts;
using JobPortalApp.Model;

namespace JobPortalApp.Domain
{
    /// <summary>
    /// Candidate Filter Class provides functionality to filter candidates based on filter not only skills, all other filter scenarios can be extend
    /// </summary>
    public class CandidateFilter : IFilter<Candidate>
    {
        /// <summary>
        /// Filters the specified candidates.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        public async Task<List<Candidate>> Filter(List<Candidate> candidates, ISpecification<Candidate> specification)
        {
            if (candidates == null)
                return null;

            var filteredCandidates = candidates.Where(specification.IsSatisfied).ToList();

            return filteredCandidates.OrderByDescending(p => p.SkillsMatchCount).ToList();
        }

    }
}