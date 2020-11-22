using System;
using Xunit;
using MailBox;
using MailBox.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTest
{
    public class UserControllerTest
    {
        [Fact]
        public void ReturnedValueOfIndexTest()
        {
            var controller = new UserController();
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
