using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.UserValidators;
using MailBox.Models.UserModels;
using System.Text;

namespace UnitTests.ValidatorsTest.UserValidationsTests
{
    public class NewUserValidatorTest
    {
        [Fact]
        public void WhenNewUserIsOk_ShouldNotHaveAnyError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                string name = "testname";
                string surname = "testsurname";
                string address = "test@address.com";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenNewUserNameIsEmpty_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                string name = "";
                string surname = "testsurname";
                string address = "test@address.com";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Name);
            #endregion
        }

        [Fact]
        public void WhenNewUserSurnameIsEmpty_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                string name = "testname";
                string surname = "";
                string address = "test@address.com";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Surname);
            #endregion
        }

        [Fact]
        public void WhenNewUserAddressIsEmpty_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                string name = "testname";
                string surname = "testsurname";
                string address = "";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenNewUserAddressHaveNoAt_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                string name = "testname";
                string surname = "testsurname";
                string address = "testaddress.com";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenNewUserAddressHaveNoDomain_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                string name = "testname";
                string surname = "testsurname";
                string address = "test@address";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenNewUserNameIsTooLong_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables
                StringBuilder stringBuilder = new StringBuilder('x');
                for(int i=0;i<validator.nameMaxLength + 1;i++)
                {
                    stringBuilder.Append('x');
                }
                string name = stringBuilder.ToString();
                string surname = "testsurname";
                string address = "test@address.com";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Name);
            #endregion
        }

        [Fact]
        public void WhenNewUserSurnameIsTooLong_ShouldtHaveError()
        {
            var validator = new NewUserValidator();
            #region Init variables  
                string name = "testname";
                StringBuilder stringBuilder = new StringBuilder('x');
                for (int i = 0; i < validator.surnameMaxLength + 1; i++)
                {
                    stringBuilder.Append('x');
                }
                string surname = stringBuilder.ToString();
                string address = "test@address.com";
            #endregion
            NewUser mailReadUpdate = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Surname);
            #endregion
        }
    }
}
