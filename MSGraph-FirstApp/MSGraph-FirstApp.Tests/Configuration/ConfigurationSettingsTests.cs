using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGraph_FirstApp.Configuration;

namespace MSGraph_FirstApp.Tests.Configuration
{
    [TestClass]
    public class ConfigurationSettingsTests
    {
        [TestMethod]
        public void AppIdKey()
        {
            ConfigurationSettings.ApplicationClientId.Should().Be("ApplicationClientId");
        }

        [TestMethod]
        public void ScopesKey()
        {
            ConfigurationSettings.Scopes.Should().Be("Scopes");
        }
    }
}
