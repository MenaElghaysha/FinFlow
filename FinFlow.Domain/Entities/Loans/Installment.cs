namespace FinFlow.Domain.Entities.Loans;

public class Installment
{
    public Guid Id { get; set; }

    public Guid LoanApplicationId { get; set; }

    public DateTime DueDate { get; set; }

    public decimal Amount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime? PaidAt { get; set; }
    // Navigation
    public LoanApplication LoanApplication { get; set; } = null!;
}