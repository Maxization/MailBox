using System;
using Xunit;
using MailBox;
using MailBox.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    public class MailControllerTests
    {
        [Fact]
        public void ReturnedValueOfIndexTest()
        {
            var controller = new MailController();
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
