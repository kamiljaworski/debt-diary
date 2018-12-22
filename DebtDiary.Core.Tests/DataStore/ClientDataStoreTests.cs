using NUnit.Framework;
using System.Collections.Generic;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class ClientDataStoreTests
    {
        [Test]
        public void TestLoginUserWithValidUser()
        {
            IClientDataStore clientDataStore = new ClientDataStore();
            User user = new User();
            clientDataStore.LoginUser(user);

            Assert.AreEqual(user, clientDataStore.LoggedUser);
        }

        [Test]
        public void TestLoginUserWithNull()
        {
            IClientDataStore clientDataStore = new ClientDataStore();
            clientDataStore.LoginUser(null);

            Assert.AreEqual(null, clientDataStore.LoggedUser);
        }

        [Test]
        public void TestLogoutUser()
        {
            IClientDataStore clientDataStore = new ClientDataStore();
            clientDataStore.LogoutUser();

            Assert.AreEqual(null, clientDataStore.LoggedUser);
        }
    }
}
