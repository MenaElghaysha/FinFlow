namespace FinFlow.Application.DTOs.Customers;

public class CreateCustomerRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public decimal MonthlyIncome { get; set; }
}