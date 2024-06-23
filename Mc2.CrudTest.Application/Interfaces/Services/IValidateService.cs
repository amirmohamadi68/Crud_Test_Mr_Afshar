using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Interfaces.Services
{
    public interface IValidateService
    {
        public Task<bool> CheckCustomerExsistByFullName(string firstname, string lastname, DateTime datteBitrh);
        public Task<bool> CheckCustomerExsistByEmail(string email);

    }

    public class ValidataService : IValidateService
    {
        private readonly IDbContext _dbContext;

        public ValidataService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> CheckCustomerExsistByFullName(string firstname, string lastname, DateTime datteBitrh)
        {
            var _firstName = FirstName.Create(firstname);
            var _lastName = LastName.Create(lastname);
            var _dateOfBirth = DateOfBirth.Create(datteBitrh);

            var res = await _dbContext.Customers.Where(w =>
                    w.FirstName == _firstName && w.LastName == _lastName && w.DateOfBirth == _dateOfBirth)
                .FirstOrDefaultAsync();
            if (res == null)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> CheckCustomerExsistByEmail(string email)
        {
            var _email = Email.Create(email); 
            var res = await _dbContext.Customers.Where(w => w.Email == _email )
                .FirstOrDefaultAsync();
            if (res == null)
            {
                return true;
            }
            return false;

        }

   
    }
}
