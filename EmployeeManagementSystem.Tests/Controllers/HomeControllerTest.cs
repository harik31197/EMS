using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;
using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Helper_Classes;

namespace EmployeeManagementSystem.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            UserInfo test = new UserInfo();
            test.username = "sourav";
            test.password = "admin";
            var result = DatabaseOps.TryLogin(test.username, test.password, out UserInfo user);
            Assert.IsTrue(result == "Success");
        }

            
            // Arrange
           // HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
