
namespace FinFlow.Application.DTOs.Loans;

public class CreateLoanApplicationRequest
{
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public int DurationInMonths { get; set; }
    public string Purpose { get; set; } = string.Empty;
}