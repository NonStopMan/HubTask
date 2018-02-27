
namespace HUB.CrossCutting.Identity
{
    /// <summary>
    /// Base contract for HUBIdentity abstract factory
    /// </summary>
    public interface IIdentityProviderFactory
    {
        /// <summary>
        /// Create a new IHUBIdentity
        /// </summary>
        /// <returns>
        /// The IHUBIdentity created
        /// </returns>
        IIdentityProvider Create();
    }
}
