namespace FinFlow.Application.DTOs.Loans;

public class InstallmentDto
{
    public Guid Id { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
}