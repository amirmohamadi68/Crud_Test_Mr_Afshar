using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Core;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Qery
{
    public class GetCustomerQuery : IRequest<GenericRespons<Customer>>
    {
        public CustomerDTO CustomerDTO { get; set; }
        public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GenericRespons<Customer>>
        {
            private readonly IDbContext _dbContext;

            public GetCustomerQueryHandler(IDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public Task<GenericRespons<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {
                //better way is using dapper unforchnaly i dont have time
                var RequestCustomerId = new CustomerId(request.CustomerDTO.Id);
                var customerResult = _dbContext.Customers.Where(w => w.Id == RequestCustomerId).FirstOrDefault();
                if (customerResult == null) { throw new Exception("Customer is not Exsesit"); }
                return Task.FromResult(new GenericRespons<Customer>(200, "", false, customerResult));

                // better way is using dapper unfurchnly i dnt have time


            }
        }
    }
}
