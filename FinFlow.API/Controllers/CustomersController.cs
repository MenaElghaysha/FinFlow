using FinFlow.Application.DTOs.Customers;
using FinFlow.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FinFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IValidator<CreateCustomerRequest> _validator;

    public CustomersController(ICustomerService customerService, IValidator<CreateCustomerRequest> validator)
    {
        _customerService = customerService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            }));
        }

        var customerId = await _customerService.CreateCustomerAsync(request);
        return Ok(new { id = customerId });    }
}

