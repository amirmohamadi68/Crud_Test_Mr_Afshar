using FluentValidation;
using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Domain.Customers;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Behaivior
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly IValidateService _validateService;

        public UpdateCustomerValidator(IValidateService validateService)
        {
            _validateService = validateService;
            RuleFor(f => f.customerDTO!.PhoneNumber).MustAsync(ValidatePhoneNumber)
                .WithMessage("Phone number is not local valid mobile number");
            RuleFor(w => w.customerDTO).MustAsync(ValidateFullname)
                .WithMessage("FirstName and LastName and DateBirth Duplicated");
            RuleFor(f => f.customerDTO.Email).MustAsync(ValidateEmailUniqe)
                .WithMessage("Email must be Uniq in Database");
        }


        private async Task<bool> ValidateEmailUniqe(string email, CancellationToken arg2)
        {
            var res = await _validateService.CheckCustomerUinqeByEmail(email);
            return res;
        }

        private async Task<bool> ValidateFullname(CustomerDTO arg1, CancellationToken arg2)
        {
            var duplicate = await
                _validateService.CheckCustomerUniqeFullName(arg1.FirstName, arg1.LastName, arg1.DateOfBirth);
            return duplicate;
        }

        private async Task<bool> ValidatePhoneNumber(string? PhoneNumber, CancellationToken token)
        {
            if (PhoneNumber == null) throw new ArgumentNullException(nameof(PhoneNumber));
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try

            {
                var result = await Task.Run(() => phoneNumberUtil.IsPossibleNumber(PhoneNumber.ToString(), "IR"));

                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
