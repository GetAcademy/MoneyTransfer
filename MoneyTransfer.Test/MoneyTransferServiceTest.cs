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

        [Test]
        public void TestTransferWithoutEnoughMoney()
        {
            // arrange = forberede, sette opp alt
            var accountA = new Account { AccountNumber = "1" };
            var accountB = new Account { AccountNumber = "2" };
            accountA.Deposit(200);
            var repo = new FakeAccountRepository(accountA, accountB);
            var service = new MoneyTransferService(repo);

            // act = gjøre det som skal testes
            Assert.Throws<InvalidOperationException>(() => service.Transfer("1", "2", 500));

            // assert = sjekke om det gikk som det skulle
            Assert.That(accountA.Balance, Is.EqualTo(200));
            Assert.That(accountB.Balance, Is.EqualTo(0));
        }

        [Test]
        public void TestNonExistantAccount()
        {
            // arrange = forberede, sette opp alt
            var repo = new FakeAccountRepository();
            var service = new MoneyTransferService(repo);

            // act = gjøre det som skal testes
            var exception = Assert.Throws<InvalidOperationException>(() => service.Transfer("1", "2", 500));

            // Assert against the exception properties
            Assert.That(exception.Message, Is.EqualTo("Kunne ikke lese konto."));
        }

        [Test]
        public void TestNegativeAmount()
        {
            // arrange = forberede, sette opp alt
            var repo = new FakeAccountRepository();
            var service = new MoneyTransferService(repo);

            // act = gjøre det som skal testes
            var exception = Assert.Throws<ArgumentException>(() => service.Transfer("1", "2", -500));

            // Assert against the exception properties
            Assert.That(exception.Message, Is.EqualTo("Beløpet må være større enn null."));
        }
    }
}
