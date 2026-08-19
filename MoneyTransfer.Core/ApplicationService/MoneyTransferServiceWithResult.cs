using MoneyTransfer.Core.DomainModel;
using MoneyTransfer.Core.DomainServices;

namespace MoneyTransfer.Core.ApplicationService
{
    public class MoneyTransferServiceWithResult
    {
        private readonly IAccountRepository _accountRepository;

        public MoneyTransferServiceWithResult(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Result<FromAndToAccount> Transfer(
            string fromAccountNumber,
            string toAccountNumber,
            decimal amount)
        {
            if (amount <= 0) return Result<FromAndToAccount>.Failure("Beløpet må være større enn null.");

            if (fromAccountNumber == toAccountNumber)
            {
                return Result<FromAndToAccount>.Failure("Fra-konto og til-konto kan ikke være den samme.");
            }

            var fromAccount = _accountRepository.Get(fromAccountNumber);
            var toAccount = _accountRepository.Get(toAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                return Result<FromAndToAccount>.Failure("Kunne ikke lese konto.");
            }

            if (fromAccount.Balance < amount)
            {
                return Result<FromAndToAccount>.Failure("Det er ikke nok penger på kontoen.");
            }

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
            //fromAccount.Balance -= amount;
            //toAccount.Balance += amount;

            _accountRepository.CreateOrUpdate(fromAccount);
            _accountRepository.CreateOrUpdate(toAccount);

            return Result<FromAndToAccount>.Success(new FromAndToAccount(fromAccount, toAccount));
        }
    }
}
