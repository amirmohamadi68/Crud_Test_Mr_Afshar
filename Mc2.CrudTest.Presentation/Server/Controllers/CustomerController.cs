using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Application.Customers.Qery;
using Mc2.CrudTest.Domain.Core;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        private readonly IMediator mediator;

        [HttpPost("~/customer")]
        public async Task<Response> CreateCustomer([FromBody] CustomerDTO request, CancellationToken cancellationToken)
        {

            var a = await mediator.Send(new CreateCustomerCommand { customerDTO = request });
            return a;
        }
        [HttpPut("~/customer")]

        public async Task<Response> UpdateCustomer([FromBody] CustomerDTO request, CancellationToken cancellationToken)
        {

            var a = await mediator.Send(new UpdateCustomerCommand { customerDTO = request });
            return a;
        }
        [HttpGet("~/customer/{id}")]
        public async Task<GenericRespons<Customer>> GetCustomerById(Guid Id, CancellationToken cancellationToken)
        {
            var a = await mediator.Send(new GetCustomerQuery { CustomerId = Id });
            return a;

        }

        [HttpDelete("~/Customer/{Id}")]
        public async Task<Response> RemoveCustomerById(Guid Id, CancellationToken cancellationToken)
        {

            var result = await mediator.Send(new RemoveCustomerCommand { Guid = Id });
            return result;
        }
    }
}
