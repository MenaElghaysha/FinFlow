using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities.Loans;
using FinFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Infrastructure.Persistence;

public class LoanRepository : ILoanRepository
{
    private readonly FinFlowDbContext _context;

    public LoanRepository(FinFlowDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LoanApplication loanApplication)
    {
        await _context.LoanApplications.AddAsync(loanApplication);
    }

    public async Task<List<LoanApplication>> GetAllAsync()
    {
        return await _context.LoanApplications.Include(l => l.Installments).ToListAsync();
    }

    public async Task<LoanApplication?> GetByIdAsync(Guid id)
    {
        return await _context.LoanApplications
        .Include(l => l.Installments)
                .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LoanApplication loanApplication)
    {
        _context.LoanApplications.Update(loanApplication);
        await Task.CompletedTask;
    }

    public async Task AddInstallmentsAsync(List<Installment> installments)
    {
        var orderedInstallments = installments
            .OrderBy(i => i.DueDate)
            .ToList();

        await _context.Installments.AddRangeAsync(orderedInstallments);
    }
    
    public async Task<Installment?> GetInstallmentByIdAsync(Guid id)
{
    return await _context.Installments
        .FirstOrDefaultAsync(i => i.Id == id);
}
}