using FinFlow.Application.DTOs.Loans;
using FinFlow.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FinFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly IValidator<CreateLoanApplicationRequest> _validator;

    public LoansController(ILoanService loanService, IValidator<CreateLoanApplicationRequest> validator)
    {
        _loanService = loanService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] CreateLoanApplicationRequest request)
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

        var loanId = await _loanService.CreateLoanApplicationAsync(request);
        return CreatedAtAction(nameof(GetLoanById), new { id = loanId }, loanId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLoanById(Guid id)
    {
        var loan = await _loanService.GetLoanByIdAsync(id);

        if (loan is null)
            return NotFound();

        return Ok(loan);
    }


    [HttpPut("{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id)
    {
        try
        {
            await _loanService.ApproveLoanAsync(id);
            return Ok(new { message = "Loan approved successfully" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Loan not found" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPut("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id)
    {
        try
        {
            await _loanService.RejectLoanAsync(id);
            return Ok(new { message = "Loan rejected successfully" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Loan not found" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("installments/{id}/pay")]
    public async Task<IActionResult> PayInstallment(Guid id)
    {
        try
        {
            await _loanService.PayInstallmentAsync(id);
            return NoContent(); // 204
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}