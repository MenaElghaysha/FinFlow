namespace FinFlow.API.Requests;

public sealed record CreateLoanApplicationRequest(
    Guid CustomerId,
    decimal Amount,
    int DurationInMonths,
    string Purpose
);