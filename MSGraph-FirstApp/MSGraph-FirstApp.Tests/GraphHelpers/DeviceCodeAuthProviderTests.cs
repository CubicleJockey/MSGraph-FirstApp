using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGraph_FirstApp.GraphHelpers;

namespace MSGraph_FirstApp.Tests.GraphHelpers
{
    [TestClass]
    public class DeviceCodeAuthProviderTests
    {
        //TODO: Make classes in a way from sample so they can be injectable
        [Ignore]
        [TestMethod]
        public void InheritsFromIAuthenticationProvider()
        {
            var provider = new DeviceCodeAuthProvider(A.Dummy<Guid>(), A.Dummy<IEnumerable<string>>());

            provider.Should().BeAssignableTo<IAuthenticationProvider>();
        }
    }
}
