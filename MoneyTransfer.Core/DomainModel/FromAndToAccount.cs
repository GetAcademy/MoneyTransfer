namespace MoneyTransfer.Core.DomainModel
{
    public class FromAndToAccount
    {
        public Account FromAccount { get; }
        public Account ToAccount { get; }

        public FromAndToAccount(Account fromAccount, Account toAccount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
        }
    }
}
