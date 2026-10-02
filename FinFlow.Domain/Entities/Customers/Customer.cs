using FinFlow.Domain.Entities.Loans;
namespace FinFlow.Domain.Entities.Customers;

public class Customer
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string NationalId { get; set; } = string.Empty;

    public decimal MonthlyIncome { get; set; }



    // Navigation
    public ICollection<LoanApplication> LoanApplications { get; set; }
        = new List<LoanApplication>();

}