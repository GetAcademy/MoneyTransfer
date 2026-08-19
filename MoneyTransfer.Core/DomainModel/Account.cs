namespace MoneyTransfer.Core.DomainModel
{
    public class Account
    {
        public string AccountNumber { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public decimal Balance { get; private set; }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}
