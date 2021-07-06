using JobPortalApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using JobPortalApp.Model;

namespace JobPortalApp.Data
{

    public class DataGenerator
    {
        /// <summary>
        /// Initializes the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CandidateContext(
                serviceProvider.GetRequiredService<DbContextOptions<CandidateContext>>()))
            {
                if (context.Candidates.Any())
                {
                    return; // Data was already seeded
                }

                //Adding Default Candidate
                var products = new List<Candidate>
                {
                    new Candidate()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Manikanta Pattigulla",
                        Skills = new List<Skills> {new Skills { Name = ".net"}}
                    }
                };


                context.Candidates.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}