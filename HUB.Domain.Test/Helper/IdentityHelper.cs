using HUB.CrossCutting.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hub.Domain.Test.Helper
{
    public static class IdentityHelper
    {
        internal static void GetIdentityProviderMock()
        {
            var identityProviderMock = new Mock<IIdentityProvider>();
            identityProviderMock.Setup(mock => mock.GetCurrentIdentity()).Returns("Test User");

            var identityProviderFactoryMock = new Mock<IIdentityProviderFactory>();
            identityProviderFactoryMock.Setup(mock => mock.Create()).Returns(identityProviderMock.Object);

            IdentityProviderFactory.SetCurrent(identityProviderFactoryMock.Object);
        }
    }
}
