using FinFlow.API.Requests;
using FinFlow.Application.Customers.Commands.CreateCustomer;
using FinFlow.Application.Customers.Queries.GetAllCustomers;
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

    [HttpGet]
public async Task<IActionResult> GetAll()
{
    var customers = await mediator.Send(new GetAllCustomersQuery());
    return Ok(customers);
}
}