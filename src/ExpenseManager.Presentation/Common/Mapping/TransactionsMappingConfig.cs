using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransactions;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Application.Transactions.Queries.SearchTransactions;
using ExpenseManager.Presentation.Contracts.Transactions;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class TransactionsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransactionResult, TransactionResponse>()
            .Map(dest => dest.CategoryIds, src => src.Transaction.Categories.Select(c => c.Id))
            .Map(dest => dest.CategoryNames, src => src.Transaction.Categories.Select(c => c.Name))
            .Map(dest => dest, src => src.Transaction);

        config.NewConfig<(CreateTransactionRequest Request, Guid UserId), CreateTransactionCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(UpdateTransactionRequest Request, Guid Id, Guid UserId), UpdateTransactionCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(RemoveTransactionsRequest Request, Guid UserId), RemoveTransactionsCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(SearchTransactionsRequest Request, Guid UserId), SearchTransactionsQuery>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}