using System;
using System.Collections;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGraph_FirstApp.GraphHelpers;

namespace MSGraph_FirstApp.Tests.GraphHelpers
{
    [TestClass]
    public class DeviceCodeAuthProviderTests
    {
        private readonly IAccount fakeUserAccount;
        private readonly IPublicClientApplication fakeMsaClientApplication;

        public DeviceCodeAuthProviderTests()
        {
            fakeUserAccount = A.Fake<IAccount>();
            fakeMsaClientApplication = A.Fake<IPublicClientApplication>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Fake.ClearConfiguration(fakeUserAccount);
            Fake.ClearRecordedCalls(fakeUserAccount);

            Fake.ClearConfiguration(fakeMsaClientApplication);
            Fake.ClearRecordedCalls(fakeMsaClientApplication);
        }

        #region Two Parameter Constructor Tests

        [TestMethod]
        public void TwoParameterConstructorApplicationClientIdIsEmpty()
        {
            Action ctor = () => _ = new DeviceCodeAuthProvider(Guid.Empty, A.Dummy<IEnumerable<string>>());
            ctor.Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot be empty. (Parameter 'applicationClientId')");
        }

        [TestMethod]
        public void TwoParameterConstructorNullScopes()
        {
            IEnumerable<string> scopes = null;

            Action ctor = () => _ = new DeviceCodeAuthProvider(Guid.NewGuid(), scopes);
            ctor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'scopes')");
        }

        #endregion Two Parameter Constructor Tests

        #region Four Parameter Constructor Tests

        [TestMethod]
        public void FourParameterConstructorUserAccountIsNull()
        {
            IAccount invalidUserAccount = null;
            
            Action ctor = () => _ = new DeviceCodeAuthProvider(invalidUserAccount, 
                                                               fakeMsaClientApplication,
                                                               Guid.NewGuid(),
                                                               A.Dummy<IEnumerable<string>>());

            ctor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'userAccount')");
        }

        [TestMethod]
        public void FourParameterConstructorMsaClientApplicationIsNull()
        {
            IPublicClientApplication invalidMsaClientApplication = null;

            Action ctor = () => _ = new DeviceCodeAuthProvider(fakeUserAccount,
                invalidMsaClientApplication,
                Guid.NewGuid(),
                A.Dummy<IEnumerable<string>>());

            ctor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'msaClientApplication')");
        }

        [TestMethod]
        public void FourParameterConstructorApplicationClientIdIsEmpty()
        {
            Action ctor = () => _ = new DeviceCodeAuthProvider(fakeUserAccount,
                fakeMsaClientApplication,
                Guid.Empty,
                A.Dummy<IEnumerable<string>>());

            ctor.Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot be empty. (Parameter 'applicationClientId')");
        }

        [TestMethod]
        public void FourParameterConstructorScopesIsNull()
        {
            Action ctor = () => _ = new DeviceCodeAuthProvider(fakeUserAccount,
                fakeMsaClientApplication,
                Guid.NewGuid(),
                null);

            ctor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'scopes')");
        }


        #endregion Four Parameter Constructor Tests

        [TestMethod]
        public void InheritsFromIAuthenticationProvider()
        {
            var provider = new DeviceCodeAuthProvider(fakeUserAccount,
                                                      fakeMsaClientApplication,
                                                      Guid.NewGuid(),
                                                      A.Dummy<IEnumerable<string>>());

            provider.Should().BeAssignableTo<IAuthenticationProvider>();
        }
    }
}
