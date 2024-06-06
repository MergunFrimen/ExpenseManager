using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransaction;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Presentation.Contracts.Transactions;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class TransactionsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateTransactionRequest Request, Guid UserId), CreateTransactionCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(Guid UserId, Guid TransactionId), RemoveTransactionCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.TransactionId, src => src.TransactionId);

        config
            .NewConfig<(UpdateTransactionRequest Request, Guid UserId, Guid TransactionId),
                UpdateTransactionCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.TransactionId, src => src.TransactionId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<TransactionResult, TransactionResponse>()
            .Map(dest => dest, src => src.Transaction);
    }
}