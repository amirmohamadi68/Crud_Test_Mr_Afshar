
using Mc2.CrudTest.Domain.Core;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class RemoveCustomerCommand : IRequest<Response>
    {
        public CustomerDTO customerDTO { get; set; }

        public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, Response>
        {
            private readonly IDbContext _dbContext;

            public RemoveCustomerCommandHandler(IDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            public async Task<Response> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
            {
                await using (var trans = await _dbContext.datbase.BeginTransactionAsync(cancellationToken))
                {

                    // i should use Specification to create query and avoid creating thos object for checking equlity
                    var requestId = new CustomerId(request.customerDTO.Id);
                    var customer =await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == requestId);
                    if (customer == null)
                    {
                        throw new Exception("there is no customer with that Id to Remove");

                    }
                    // i should move this logic to repository and make that async
                     _dbContext.Customers.Remove(customer);
                    ;
                     
                    await _dbContext.SaveChangesAsync(cancellationToken);
                  var  _response = Response.Create(201, "Customer Updated", true) ?? throw new NullReferenceException();
                    await trans.CommitAsync(cancellationToken);
                    return _response;
                }
            }
        }
    }
}
