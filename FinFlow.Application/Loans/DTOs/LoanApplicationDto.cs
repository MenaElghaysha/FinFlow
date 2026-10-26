namespace FinFlow.Application.Loans.DTOs;

public sealed record LoanApplicationDto(
    Guid Id,
    Guid CustomerId,
    decimal Amount,
    int DurationInMonths,
    string Purpose,
    string Status,
    DateTime CreatedAt,
    List<InstallmentDto> Installments
);

public sealed record InstallmentDto(
    Guid Id,
    DateTime DueDate,
    decimal Amount,
    bool IsPaid,
    DateTime? PaidAt
);