using MoneyTransfer.DI.Console.Model;

namespace MoneyTransfer.DI.Console.DomainServices
{
    public interface IAccountRepository
    {
        Account? Get(string accountNo);

        void CreateOrUpdate(Account account);
    }
}
