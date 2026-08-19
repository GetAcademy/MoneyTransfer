using MoneyTransfer.Core.DomainModel;
using MoneyTransfer.Core.DomainServices;

namespace MoneyTransfer.Test
{
    internal class FakeAccountRepository : IAccountRepository
    {
        private readonly Dictionary<string, Account> _accounts;

        public FakeAccountRepository(params Account[] accounts)
        {
            _accounts = accounts.ToDictionary(a => a.AccountNumber);
        }

        public Account? Get(string accountNo)
        {
            return _accounts.ContainsKey(accountNo) ? _accounts[accountNo] : null;
        }

        public void CreateOrUpdate(Account account)
        {
            _accounts[account.AccountNumber] = account;
        }
    }
}
