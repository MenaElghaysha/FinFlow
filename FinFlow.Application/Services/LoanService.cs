using FinFlow.Application.DTOs.Loans;
using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities.Loans;

namespace FinFlow.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;


    // Constructor to inject the ILoanRepository dependency
    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<Guid> CreateLoanApplicationAsync(CreateLoanApplicationRequest request)
    {
        var loan = new LoanApplication
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            Amount = request.Amount,
            DurationInMonths = request.DurationInMonths,
            Purpose = request.Purpose,
            Status = LoanStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _loanRepository.AddAsync(loan); // tell the repo to add the loan application in the DB context
        await _loanRepository.SaveChangesAsync();  // persist the changes to the database

        return loan.Id;
    }

    public async Task<List<LoanApplicationDto>> GetAllLoansAsync()
    {
        var loans = await _loanRepository.GetAllAsync();

        return loans.Select(l => new LoanApplicationDto
        {
            Id = l.Id,
            CustomerId = l.CustomerId,
            Amount = l.Amount,
            DurationInMonths = l.DurationInMonths,
            Purpose = l.Purpose,
            Status = l.Status.ToString(),
            CreatedAt = l.CreatedAt,
            Installments = l.Installments.Select(i => new InstallmentDto
            {
                Id = i.Id,
                DueDate = i.DueDate,
                Amount = i.Amount,
                IsPaid = i.IsPaid,
                PaidAt = i.PaidAt
            }).ToList()
        }).ToList();
    }

    public async Task<LoanApplicationDto?> GetLoanByIdAsync(Guid id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);

        if (loan is null)
            return null;

        return new LoanApplicationDto
        {
            Id = loan.Id,
            CustomerId = loan.CustomerId,
            Amount = loan.Amount,
            DurationInMonths = loan.DurationInMonths,
            Purpose = loan.Purpose,
            Status = loan.Status.ToString(),
            CreatedAt = loan.CreatedAt,
            Installments = loan.Installments.Select(i => new InstallmentDto
            {
                Id = i.Id,
                DueDate = i.DueDate,
                Amount = i.Amount,
                IsPaid = i.IsPaid,
                PaidAt = i.PaidAt
            }).ToList()
        };
    }

    public async Task ApproveLoanAsync(Guid id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);

        if (loan is null)
            throw new KeyNotFoundException("Loan not found");

        if (loan.Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loans can be approved");

        // 1. Change status
        loan.Status = LoanStatus.Approved;

        // 2. Generate Installments as separate list
        var installments = new List<Installment>();
        var installmentAmount = Math.Round(loan.Amount / loan.DurationInMonths, 2);

        for (int i = 1; i <= loan.DurationInMonths; i++)
        {
            installments.Add(new Installment
            {
                Id = Guid.NewGuid(),
                LoanApplicationId = loan.Id,
                Amount = installmentAmount,
                DueDate = loan.CreatedAt.AddMonths(i),
                IsPaid = false
            });
        }

        // 3. Add installments through repository
        await _loanRepository.AddInstallmentsAsync(installments);

        // 4. Update the loan status
        await _loanRepository.UpdateAsync(loan);
        await _loanRepository.SaveChangesAsync();
    }

    public async Task RejectLoanAsync(Guid id)
    {
        LoanApplication? loan = await _loanRepository.GetByIdAsync(id);

        if (loan is null)
            throw new KeyNotFoundException("Loan not found");

        if (loan.Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loans can be rejected");

        loan.Status = LoanStatus.Rejected;

        // await _loanRepository.UpdateAsync(loan);
        await _loanRepository.SaveChangesAsync();
    }

    private void GenerateInstallments(LoanApplication loan)
    {
        var installmentAmount = Math.Round(loan.Amount / loan.DurationInMonths, 2);

        for (int i = 1; i <= loan.DurationInMonths; i++)
        {
            var installment = new Installment
            {
                Id = Guid.NewGuid(),
                Amount = installmentAmount,
                DueDate = loan.CreatedAt.AddMonths(i),
                IsPaid = false
            };

            loan.Installments.Add(installment);
        }
    }

public async Task PayInstallmentAsync(Guid installmentId)
{
    var installment = await _loanRepository.GetInstallmentByIdAsync(installmentId);

    if (installment is null)
        throw new KeyNotFoundException("Installment not found");

    if (installment.IsPaid)
        throw new InvalidOperationException("Installment is already paid");

        installment.IsPaid = true;
        installment.PaidAt = DateTime.UtcNow;
    await _loanRepository.SaveChangesAsync();
}
}