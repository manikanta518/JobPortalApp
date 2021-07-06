using System;
using System.Collections.Generic;
using System.Linq;
using JobPortalApp.Domain.Contracts;
using JobPortalApp.Model;

namespace JobPortalApp.Domain
{
    /// <summary>
    /// Skills Specification Class provides filter candidates based on skills
    /// </summary>
    public class SkillsSpecification : ISpecification<Candidate>
    {
        private readonly List<string> _skills;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsSpecification"/> class.
        /// </summary>
        /// <param name="skills">The skills.</param>
        public SkillsSpecification(List<string> skills)
        {
            _skills = skills;
        }


        /// <summary>
        /// Determines whether the specified candidate is satisfied.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        ///   <c>true</c> if the specified candidate is satisfied; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSatisfied(Candidate candidate)
        {
            var candidateSkills = candidate.Skills.Select(p => p.Name).ToList();
           
            var resultList = candidateSkills.Where(i => _skills.Contains(i, StringComparer.OrdinalIgnoreCase)).ToList();

            candidate.SkillsMatchCount = resultList.Count;

            return resultList.Any();
        }
    }
}