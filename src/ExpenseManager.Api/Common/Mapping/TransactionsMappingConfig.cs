using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Contracts.Transactions;
using Mapster;

namespace ExpenseManager.Api.Common.Mapping;

public class TransactionsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTransactionRequest, CreateTransactionCommand>();

        config.NewConfig<TransactionResult, TransactionResponse>()
            .Map(dest => dest.Id, src => src.Transaction.Id)
            .Map(dest => dest.Type, src => src.Transaction.Type.ToString())
            .Map(dest => dest.Category, src => src.Transaction.Category.Name)
            .Map(dest => dest.Description, src => src.Transaction.Description)
            .Map(dest => dest.Price, src => src.Transaction.Price.Amount)
            .Map(dest => dest.Date, src => src.Transaction.Date);
    }
}