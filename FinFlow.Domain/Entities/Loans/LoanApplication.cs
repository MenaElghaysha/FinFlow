using FinFlow.Domain.Entities.Customers;
namespace FinFlow.Domain.Entities.Loans;

public class LoanApplication
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Amount { get; set; }

    public int DurationInMonths { get; set; }

    public string Purpose { get; set; } = string.Empty;

    public LoanStatus Status { get; set; } = LoanStatus.Pending;

    public DateTime CreatedAt { get; set; }

    // Navigation
    public Customer Customer { get; set; } = null!;
    public ICollection<Installment> Installments { get; set; }
        = new List<Installment>();
}