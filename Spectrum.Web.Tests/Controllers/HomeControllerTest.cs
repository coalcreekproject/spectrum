using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectrum.Web.Controllers;
using Spectrum.Web.Controllers.Web;

namespace Spectrum.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}