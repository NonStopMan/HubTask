using System;
using System.Security.Claims;
using System.Security.Principal;
using HUB.CrossCutting.Identity;

namespace HUB.CrossCutting.Framework.Identity
{
    public class CLPIdentity : IIdentityProvider
    {
        private readonly IPrincipal _principal;

        #region Constructors

        public CLPIdentity(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));
            _principal = principal;
        }

        #endregion

        #region ICLPIdentity Members

        public string GetCurrentIdentity()
        {
            foreach (var item in ((ClaimsIdentity)_principal.Identity).Claims)
            {
                if (item.Type == "name")
                {
                    return item.Value;//.Split(" ")[1];
                }
            }
            return _principal.Identity.Name;
        }

        public string GetCurrentIdentityEmail()
        {

            return _principal.Identity.Name;
        }

        public bool IsInRole(AccessLevel accessLevel)
        {
            return _principal.IsInRole(Enum.GetName(typeof(AccessLevel), accessLevel));
        }

        #endregion
    }
}