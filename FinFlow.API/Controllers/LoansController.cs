using FinFlow.API.Requests;
using FinFlow.Application.Loans.Commands.ApproveLoan;
using FinFlow.Application.Loans.Commands.CreateLoanApplication;
using FinFlow.Application.Loans.Commands.PayInstallment;
using FinFlow.Application.Loans.Commands.RejectLoan;
using FinFlow.Application.Loans.Queries.GetAllLoans;
using FinFlow.Application.Loans.Queries.GetLoanById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLoanApplicationRequest request)
    {
        var command = new CreateLoanApplicationCommand(
            request.CustomerId,
            request.Amount,
            request.DurationInMonths,
            request.Purpose
        );

        var loanId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = loanId }, new { id = loanId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var loans = await mediator.Send(new GetAllLoansQuery());
        return Ok(loans);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var loan = await mediator.Send(new GetLoanByIdQuery(id));

        if (loan is null)
            return NotFound();

        return Ok(loan);
    }

    [HttpPut("{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id)
    {
        await mediator.Send(new ApproveLoanCommand(id));
        return Ok(new { message = "Loan approved successfully" });
    }

    [HttpPut("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id)
    {
        await mediator.Send(new RejectLoanCommand(id));
        return Ok(new { message = "Loan rejected successfully" });
    }

    [HttpPut("installments/{id:guid}/pay")]
    public async Task<IActionResult> PayInstallment(Guid id)
    {
        await mediator.Send(new PayInstallmentCommand(id));
        return NoContent();
    }
}