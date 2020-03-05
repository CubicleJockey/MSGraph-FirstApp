using FluentAssertions;
using Microsoft.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSGraph_FirstApp.Tests
{
    [TestClass]
    public class DeviceCodeAuthProviderTests
    {
        [TestMethod]
        public void InheritsFromIAuthenticationProvider()
        {
            var provider = new DeviceCodeAuthProvider();

            provider.Should().BeAssignableTo<IAuthenticationProvider>();
        }
    }
}
