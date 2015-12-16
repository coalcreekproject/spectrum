using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Data.Core.Repositories.InterfaceUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectrum.Data.Core.Utilities;

namespace Spectrum.Data.Core.Repositories.InterfaceUtilities.Tests
{
    [TestClass()]
    public class PasswordUtilityTests
    {
        [TestMethod()]
        public void HashPasswordTest()
        {
            var hash = PasswordUtility.HashPassword("testpassword");
            var hash2 = PasswordUtility.HashPassword("testpassword");
            Assert.AreNotEqual(hash, hash2);
        }

        [TestMethod()]
        public void ComparePasswordToHashTest()
        {
            var hash = PasswordUtility.HashPassword("testpassword");
            var passwordValid = PasswordUtility.ComparePasswordToHash(hash, "testpassword");
            Assert.IsTrue(passwordValid);
        }
    }
}
