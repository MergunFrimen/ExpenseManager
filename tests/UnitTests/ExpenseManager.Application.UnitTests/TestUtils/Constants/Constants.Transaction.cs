using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Transaction
    {
        public const string Description = "Test Description";
        public const decimal Amount = 100;
        public const TransactionType Type = TransactionType.Income;
        public static readonly Guid Id = new("CA761232-ED42-11CE-BACD-00AA0057B223");
        public static readonly DateTimeOffset Date = new(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }
}