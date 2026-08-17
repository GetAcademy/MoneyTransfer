using System.Text.Json;
using MoneyTransfer.DI.Console.DomainServices;
using MoneyTransfer.DI.Console.Model;

namespace MoneyTransfer.DI.Console.Infrastructure
{
    internal class FileAccountRepository : IAccountRepository
    {
        public Account? Get(string accountNo)
        {
            var accountFilePath = CreateAccountFilePath(accountNo);

            if (!File.Exists(accountFilePath))
            {
                throw new FileNotFoundException($"Fant ikke kontoen {accountNo}.");
            }
            var accountJson = File.ReadAllText(accountFilePath);

            var account = JsonSerializer.Deserialize<Account>(accountJson);

            return account;
        }

        private static string CreateAccountFilePath(string accountNo)
        {
            var accountFilePath = $"accounts/{accountNo}.json";
            return accountFilePath;
        }

        public void CreateOrUpdate(Account account)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            var accountFilePath = CreateAccountFilePath(account.AccountNumber);

            var json = JsonSerializer.Serialize(account, options);
            File.WriteAllText(accountFilePath, json);
        }
    }
}
