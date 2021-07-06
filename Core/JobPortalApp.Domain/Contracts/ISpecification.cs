namespace JobPortalApp.Domain.Contracts
{
    /// <summary>
    /// ISpecification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Determines whether the specified item is satisfied.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if the specified item is satisfied; otherwise, <c>false</c>.
        /// </returns>
        bool IsSatisfied(T item);
    }
}