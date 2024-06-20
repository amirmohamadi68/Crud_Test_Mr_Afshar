using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<GenericResponse> CreateCustomer([FromBody]CustomerDTO request , CancellationToken cancellationToken)
        {

           var a =await mediator.Send(new CreateCustomerCommand { customerDTO = request });
            return a;
        }

    }
}
