namespace FinFlow.API.Requests;

public sealed record CreateCustomerRequest(
    string FullName,
    string Email,
    string NationalId,
    decimal MonthlyIncome
);