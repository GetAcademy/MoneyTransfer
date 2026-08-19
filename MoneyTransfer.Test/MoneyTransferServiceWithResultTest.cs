using MoneyTransfer.Core.ApplicationService;
using MoneyTransfer.Core.DomainModel;

namespace MoneyTransfer.Test
{
    internal class MoneyTransferServiceWithResultTest
    {
        [Test]
        public void TestTransferWithEnoughMoney()
        {
            // arrange = forberede, sette opp alt
            var accountA = new Account { AccountNumber = "1" };
            var accountB = new Account { AccountNumber = "2" };
            accountA.Deposit(700);
            var repo = new FakeAccountRepository(accountA, accountB);
            var service = new MoneyTransferServiceWithResult(repo);

            // act = gjøre det som skal testes
            var result = service.Transfer("1", "2", 500);

            // assert = sjekke om det gikk som det skulle
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(accountA.Balance, Is.EqualTo(200));
            Assert.That(accountB.Balance, Is.EqualTo(500));
        }

        [Test]
        public void TestTransferWithoutEnoughMoney()
        {
            // arrange = forberede, sette opp alt
            var accountA = new Account { AccountNumber = "1" };
            var accountB = new Account { AccountNumber = "2" };
            accountA.Deposit(200);
            var repo = new FakeAccountRepository(accountA, accountB);
            var service = new MoneyTransferServiceWithResult(repo);

            // act = gjøre det som skal testes
            var result = service.Transfer("1", "2", 500);

            // assert = sjekke om det gikk som det skulle
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Det er ikke nok penger på kontoen."));
        }
    }
}
