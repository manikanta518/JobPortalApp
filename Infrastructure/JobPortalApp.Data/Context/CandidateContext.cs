using JobPortalApp.Model;
using Microsoft.EntityFrameworkCore;

namespace JobPortalApp.Data.Context
{
    public class CandidateContext : DbContext
    {
        public CandidateContext(DbContextOptions<CandidateContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the candidates.
        /// </summary>
        /// <value>
        /// The candidates.
        /// </value>
        public DbSet<Candidate> Candidates { get; set; }

        /// <summary>
        /// Gets or sets the skills.
        /// </summary>
        /// <value>
        /// The skills.
        /// </value>
        public DbSet<Skills> Skills { get; set; }
    }
}
