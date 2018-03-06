
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msr.Infrastructure.Helpers;
using NUnit.Framework;

namespace Msr.Infrastructure.Tests.Helpers
{
    [TestClass]
    public class AuthenticationHelperTests
    {
        [TestMethod]
        public void TestPassword()
        {
            var answerPassowrd = "abc";

           var answerDecode = AuthenticationHelper.PasswordEncrypt(answerPassowrd);

            NUnit.Framework.Assert.IsTrue(answerDecode == "");
        }
    }
}
