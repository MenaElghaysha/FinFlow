namespace FinFlow.Application.DTOs.Loans;

public class LoanApplicationDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public int DurationInMonths { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public List<InstallmentDto> Installments { get; set; } = new();
}