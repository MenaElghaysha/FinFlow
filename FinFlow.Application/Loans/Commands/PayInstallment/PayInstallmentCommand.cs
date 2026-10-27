using MediatR;

namespace FinFlow.Application.Loans.Commands.PayInstallment;

public sealed record PayInstallmentCommand(Guid InstallmentId) : IRequest;