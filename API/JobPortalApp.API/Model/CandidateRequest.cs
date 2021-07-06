using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using JobPortalApp.Model;

namespace JobPortalApp.API.Model
{
    public class CandidateRequest:IValidatableObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [Required]
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the skills.
        /// </summary>
        /// <value>
        /// The skills.
        /// </value>
        [Required]
        public List<string> Skills { get; set; }


        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns></returns>
        public Candidate BuildRequest()
        {
            return new Candidate {Id = Id, Name = Name, Skills = Skills.Select(p => new Skills {Name = p}).ToList()};
        }

        /// <summary>
        /// Builds the response.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns></returns>
        public void BuildResponse(Candidate candidate)
        {
            Id = candidate.Id;
            Name = candidate.Name;
            Skills = candidate.Skills.Select(p => p.Name).ToList();
        }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Validation Check for maximum of 10,000 elements
            if (Skills.Count>10000)
                yield return new ValidationResult("Skills should be less than 10,000", new[] { nameof(Skills) });

            //Validation Check for duplicates
            var duplicates = Skills.GroupBy(x => x.ToLower())
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            if(duplicates.Any())
                yield return new ValidationResult("Duplicate items found in skills", new[] { nameof(Skills) });

            //Validation Check for letters, numbers or hyphens
            var rgx = new Regex(@"^([a-zA-Z0-9-]+)$");
            foreach (var skill in Skills)
            {
                if(!rgx.IsMatch(skill))
                    yield return new ValidationResult("Invalid Skills Data", new[] { nameof(Skills) });
            }

        }
    }
}
