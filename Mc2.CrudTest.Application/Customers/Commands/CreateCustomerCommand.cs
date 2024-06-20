using Mc2.CrudTest.Domain.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<GenericResponse>
    {
        public CustomerDTO customerDTO { get; set; }
        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, GenericResponse>
        {
            private readonly IGenericRespository<Customer> _repository;
            private readonly IDbContext _dbContext;
            private readonly IUnitOfWork _unitOfWork;
            private GenericResponse _response;
            public CreateCustomerCommandHandler(IGenericRespository<Customer> repository
             , IUnitOfWork unitOfWork, IDbContext dbContext, GenericResponse genericResponse)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _dbContext = dbContext;
                // _response = genericResponse;

            }

            public async Task<GenericResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                CheckRequest(request);
                var customerEntity = Customer.Create(request.customerDTO);

                /// not neccesary because i changed created response
                // CheckEntityCreate(customerEntity);

                await using (var trans = await _dbContext.Datbase.BeginTransactionAsync(cancellationToken))
                {
                    try
                    {
                        bool result = await _repository.AddAsync(customerEntity);
                        if (!result) throw new Exception("will be Custom later");
                        bool unitResukt = await _unitOfWork.SaveChangesAsync(); //  i will rise event in savechangs factory
                                                                                // it was better if i designing event dispatcher pattern in my entities... maybe another time
                        if (!unitResukt) throw new Exception("will be Custom later");

                        _response = GenericResponse.Create(201, "Customer Created", true) ?? throw new NullReferenceException();
                        await trans.CommitAsync(cancellationToken);
                    }
                    catch (Exception e)
                    {
                        await trans.RollbackAsync(cancellationToken);
                        _response = GenericResponse.Create(200, $"Customer Not Created : {e.Message} ", false);

                    }
                    return _response;
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

