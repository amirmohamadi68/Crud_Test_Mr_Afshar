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

    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IValidateService _validateService;

        public CreateCustomerValidator(IValidateService validateService)
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
            var res = await _validateService.CheckCustomerExsistByEmail(email);
            return res;
        }

        private async Task<bool> ValidateFullname(CustomerDTO arg1, CancellationToken arg2)
        {
            var duplicate = await
                _validateService.CheckCustomerExsistByFullName(arg1.FirstName, arg1.LastName, arg1.DateOfBirth);
            return duplicate;
        }

        private async Task<bool> ValidatePhoneNumber(string? arg1, CancellationToken token)
        {
            if (arg1 == null) throw new ArgumentNullException(nameof(arg1));
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try

            {
                var result = await Task.Run(() => phoneNumberUtil.IsPossibleNumber(arg1.ToString(), "IR"));

                return result;
            }
            catch
            {
                return false;
            }
        }
    }

}
