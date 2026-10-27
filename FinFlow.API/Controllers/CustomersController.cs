using FinFlow.API.Requests;
using FinFlow.Application.Customers.Commands.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var command = new CreateCustomerCommand(
            request.FullName,
            request.Email,
            request.NationalId,
            request.MonthlyIncome
        );

        var customerId = await mediator.Send(command);

        return Ok(new { id = customerId });
    }
}