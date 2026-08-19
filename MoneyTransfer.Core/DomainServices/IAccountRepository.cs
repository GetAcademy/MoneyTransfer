using MoneyTransfer.Core.DomainModel;

namespace MoneyTransfer.Core.DomainServices
{
    public interface IAccountRepository
    {
        Account? Get(string accountNo);

        void CreateOrUpdate(Account account);
    }
}
