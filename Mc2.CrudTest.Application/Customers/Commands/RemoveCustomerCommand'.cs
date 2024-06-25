
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
        public Guid Guid { get; set; }

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
                    var requestId = new CustomerId(request.Guid);
                    var customer =await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == requestId);
                    if (customer == null)
                    {
                        throw new Exception("there is no customer with that Id to Remove"); // i dont have time to throw Custom Exception and handle that in midlware...
                                                                                            // i did that for validating and this same as that

                    }
                    // i should move this logic to repository and make that async
                     _dbContext.Customers.Remove(customer);
                    ;
                     
                    await _dbContext.SaveChangesAsync(cancellationToken);
                  var  _response = Response.Create(200, "Customer Removed", true) ;
                    await trans.CommitAsync(cancellationToken);
                    return _response;
                }
            }
        }
    }
}
