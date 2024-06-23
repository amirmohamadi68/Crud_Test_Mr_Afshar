
using Mc2.CrudTest.Domain.Core;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Response>
    {
        public CustomerDTO customerDTO { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response>
        {
            private readonly IDbContext _dbContext;
            private readonly IRepository _repository;
            private Response _response;

            public UpdateCustomerCommandHandler(IDbContext dbContext , IRepository repository)
            {
                _dbContext = dbContext;
                _repository = repository;
                //  _response = response;
            }

            public async Task<Response> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {

                CheckRequest(request);
                var customerId = new CustomerId(request.customerDTO.Id);
                var oldCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
                await using (var trans = await _dbContext.datbase.BeginTransactionAsync(cancellationToken))
                {
                  
              

                    if (oldCustomer == null)
                    {
                        throw new Exception("there is no customer with that Id to updating");

                    }
                    oldCustomer.Update(request.customerDTO);
                    await Task.WhenAll
                            (
                             Task.Run(() => _dbContext.Customers.Update(oldCustomer))
                            );

                  await  _dbContext.SaveChangesAsync(cancellationToken);
                    //fix cuncurecy later
                  
                   // await _repository.UpdateAsync(oldCustomer);
                        _response = Response.Create(201, "Customer Updated", true) ?? throw new NullReferenceException();
                    await trans.CommitAsync(cancellationToken);
                    return _response;
                }
            }



            private void CheckRequest(UpdateCustomerCommand request)
            {
               
               if(request.customerDTO == null ) throw new ArgumentNullException("request update is null");
            }
        }

    }
}
