using MoneyTransfer.Core.DomainModel;
using System.Security.Principal;
using MoneyTransfer.Core.ApplicationService;

namespace MoneyTransfer.Test
{
    internal class MoneyTransferServiceTest
    {
        [Test]
        public void TestTransferWithEnoughMoney()
        {
            // arrange = forberede, sette opp alt
            var accountA = new Account { AccountNumber = "1" };
            var accountB = new Account { AccountNumber = "2" };
            accountA.Deposit(700);
            var repo = new FakeAccountRepository(accountA, accountB);
            var service = new MoneyTransferService(repo);

            // act = gjøre det som skal testes
            service.Transfer("1", "2", 500);

            // assert = sjekke om det gikk som det skulle
            Assert.That(accountA.Balance, Is.EqualTo(200));
            Assert.That(accountB.Balance, Is.EqualTo(500));
        }
    }
}
