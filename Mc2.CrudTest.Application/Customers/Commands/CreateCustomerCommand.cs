using Mc2.CrudTest.Domain.Core;
using Mc2.CrudTest.Domain.Customers;
using MediatR;



namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Response>
    {
        public CustomerDTO customerDTO { get; set; }
        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response>
        {
          
            private readonly IDbContext _dbContext;
            public CreateCustomerCommandHandler(IDbContext dbContext)
            {

                _dbContext = dbContext;
      
                // _response = genericResponse;

            }

            public async Task<Response> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                CheckRequest(request);
                var customerEntity = Customer.Create(request.customerDTO);

                /// not neccesary because i changed created response
                // CheckEntityCreate(customerEntity);

 
                await using (var trans = await _dbContext.datbase.BeginTransactionAsync(cancellationToken))
                {
                    try
                    {
                       var res= await _dbContext.Customers.AddAsync(customerEntity);
                        //  i will rise event in savechangs factory
                        // it was better if i designing event dispatcher pattern in my entities... maybe another time

                      await  _dbContext.SaveChangesAsync(cancellationToken);
                      var  _response = Response.Create(201, "Customer Created", true) ?? throw new NullReferenceException();
                        await trans.CommitAsync(cancellationToken);
                        return _response;
                    }
                    catch (Exception e)
                    {
                        await trans.RollbackAsync(cancellationToken);
                      var  _response = Response.Create(200, $"Customer Not Created : {e.Message} ", false);
                        return _response;

                    }
                   
                }

            }

            private static void CheckEntityCreate(Customer? customerEntity)
            {
                if (customerEntity == null)
                    throw new ArgumentNullException(nameof(customerEntity));
            }

            private static void CheckRequest(CreateCustomerCommand request)
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));
                if (request.customerDTO == null)
                    throw new ArgumentNullException(nameof(request.customerDTO));
            }
        }
    }
}

