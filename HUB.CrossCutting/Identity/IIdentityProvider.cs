
using System;

namespace HUB.CrossCutting.Identity
{

    public interface IIdentityProvider
    {
        string GetCurrentIdentity();
        string GetCurrentIdentityEmail();
        bool IsInRole(AccessLevel accessLevel);
    }
}
