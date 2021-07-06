using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortalApp.Model;

namespace JobPortalApp.Domain.Contracts
{
    /// <summary>
    /// IFilter class to filter candidates based on search conditions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T>
    {
        /// <summary>
        /// Filters the specified candidates.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        Task<List<Candidate>> Filter(List<Candidate> candidates, ISpecification<T> specification);
    }
}