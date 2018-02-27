using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using HUB.CrossCutting.Identity;
using Microsoft.AspNetCore.Http;

namespace HUB.CrossCutting.Framework.Identity
{
    public class CLPIdentityProviderFactory : IIdentityProviderFactory
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CLPIdentityProviderFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #region IIdentityProviderFactory Members
        public IIdentityProvider Create()
        {
            
            return new CLPIdentity(_httpContextAccessor.HttpContext.User);
        }

        #endregion
    }
}