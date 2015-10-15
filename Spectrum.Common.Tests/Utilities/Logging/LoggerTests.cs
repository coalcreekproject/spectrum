using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectrum.Utility.Utilities.Logging;

namespace Spectrum.Common.Tests.Utilities.Logging
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod()]
        public void ErrorTest()
        {
            Logger.SetupSemanticLoggingApplicationBlock();
            Logger.Log.Error(new Exception("Test exception message"));
            Assert.Inconclusive();
        }
    }
}