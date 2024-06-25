using Mc2.CrudTest.Application.Behaivior;
using Mc2.CrudTest.Application.Interfaces.Services;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared
{
    public class CreateCustomerValidatorTests
    {
        private readonly CreateCustomerValidator _validator;
        private readonly Mock<IValidateService> _mockValidateService;

        public CreateCustomerValidatorTests()
        {
            _mockValidateService = new Mock<IValidateService>();

            // Set up mock methods
            _mockValidateService.Setup(v => v.CheckCustomerUinqeByEmail(It.IsAny<string>()))
               .ReturnsAsync(true); // Assume the email is unique for this test by default

            _mockValidateService.Setup(v => v.CheckCustomerUniqeFullName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false); // Assume the name is not duplicated by default

            _validator = new CreateCustomerValidator(_mockValidateService.Object);
        }
        public CreateCustomerValidator GetCreateCustomerValidator()
        {
            return _validator;
        }
        public Mock<IValidateService> GetMockSercice()
        {
            return _mockValidateService;
        }
    }
}
