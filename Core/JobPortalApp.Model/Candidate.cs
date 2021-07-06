using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPortalApp.Model
{
    public class Candidate
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the skills.
        /// </summary>
        /// <value>
        /// The skills.
        /// </value>
        public ICollection<Skills> Skills { get; set; }

        /// <summary>
        /// Gets or sets the skills match count.
        /// </summary>
        /// <value>
        /// The skills match count.
        /// </value>
        public int SkillsMatchCount { get; set; }
        
    }
}