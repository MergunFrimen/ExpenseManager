using System.Transactions;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Import.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Import.Commands;
    
public record ImportCommand(
    Guid UserId,
    
    List<TransactionDto> Transactions,
    List<CategoryDto> Categories
): ICommand<ImportResult>;

public sealed record TransactionDto(
    Guid Id,
    string Description,
    decimal Amount,
    TransactionType Type,
    ulong? Date,
    List<CategoryDto> Categories
);

public sealed record CategoryDto(
    Guid Id,
    string Name
);