
using Xunit;
using MailBox.Models.MailModels;
using System.Collections.Generic;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class PagingMailInboxViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
            List<MailInboxView> mails = new List<MailInboxView>();
            bool firstPage = true;
            bool lastPage = false;
            #endregion
            PagingMailInboxView pagingMail = new PagingMailInboxView
            {
                Mails = mails,
                FirstPage = firstPage,
                LastPage = lastPage

            };
            #region Tests
            Assert.NotNull(pagingMail);
            Assert.Equal(pagingMail.Mails, mails);
            Assert.Equal(pagingMail.FirstPage, firstPage);
            Assert.Equal(pagingMail.LastPage, lastPage);
            #endregion
        }
    }
}
