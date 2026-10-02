using FinFlow.Application.DTOs.Loans;

namespace FinFlow.Application.Interfaces;

public interface ILoanService
{
    Task<Guid> CreateLoanApplicationAsync(CreateLoanApplicationRequest request);
    Task<List<LoanApplicationDto>> GetAllLoansAsync();
    Task<LoanApplicationDto?> GetLoanByIdAsync(Guid id);
    Task ApproveLoanAsync(Guid id);
    Task RejectLoanAsync(Guid id);
    Task PayInstallmentAsync(Guid installmentId);
}