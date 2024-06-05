using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Contracts.Transactions;
using Mapster;

namespace ExpenseManager.Api.Common.Mapping;

public class LedgersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateTransactionRequest Request, string LedgerId), CreateTransactionCommand>()
            .Map(dest => dest.LedgerId, src => src.LedgerId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<TransactionResult, TransactionResponse>()
            .Map(dest => dest, src => src.Transaction);
    }
}