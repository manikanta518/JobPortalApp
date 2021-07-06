using JobPortalApp.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortalApp.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using JobPortalApp.Model;

namespace JobPortalApp.Domain
{
    /// <summary>
    /// Candidate Service Provides CURD functionalists
    /// </summary>
    /// <seealso cref="ICandidateService" />
    public class CandidateService : ICandidateService
    {
        private readonly CandidateContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CandidateService(CandidateContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the candidates.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Candidate>> GetCandidates()
        {
            var skills=  _context.Skills.ToListAsync();
            var candidates= _context.Candidates.ToListAsync();
            await skills;
            return await candidates;
        }


        /// <summary>
        /// Adds the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        public async Task Add(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

    }
}