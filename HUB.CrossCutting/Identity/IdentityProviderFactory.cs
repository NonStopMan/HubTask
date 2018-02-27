using System;

namespace HUB.CrossCutting.Identity
{
    /// <summary>
    ///     CLPIdentity Factory
    /// </summary>
    public static class IdentityProviderFactory
    {
        #region Public Properties

        /// <summary>
        ///     Gets the current CLPIdentity factory.
        /// </summary>
        /// <value>
        ///     The current CLPIdentity factory.
        /// </value>
        /// 
        public static IIdentityProviderFactory CurrentCLPIdentityFactory { get; private set; }

        #endregion

        #region Members

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the current.
        /// </summary>
        /// <param name="CLPIdentityFactory">The CLPIdentity factory.</param>
        /// <exception cref="System.ArgumentNullException">CLPIdentityFactory</exception>
        public static void SetCurrent(IIdentityProviderFactory cLPIdentityFactory)
        {
            if (null == cLPIdentityFactory)
                throw new ArgumentNullException("CLPIdentityFactory");
            CurrentCLPIdentityFactory = cLPIdentityFactory;
        }

        /// <summary>
        ///     Creates the CLP identity provider.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        ///     CurrentCLPIdentityFactory is null, You must first set current
        ///     CLPIdentityger factory.
        /// </exception>
        public static IIdentityProvider CreateCLPIdentity()
        {
            if (null == CurrentCLPIdentityFactory)
                throw new InvalidOperationException(
                    "CurrentCLPIdentityFactory is null, You must first set current CLPIdentityger factory.");
            return CurrentCLPIdentityFactory.Create();
        }

        #endregion
    }
}