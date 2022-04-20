namespace ProtoType.Service.Interfaces
{
    /// <summary>
    /// Helper service to assess data from context.
    /// </summary>
    public interface IContextAccessorService
    {
        /// <summary>
        /// Returns client ID from context.
        /// </summary>
        /// <returns></returns>
        string GetCurrentClientId();
    }
}
