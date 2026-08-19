using MoneyTransfer.Core.DomainModel;

namespace MoneyTransfer.Test
{
    public class AccountTest
    {
        [Test]
        public void TestTransferWithEnoughMoney()
        {
            // arrange = forberede, sette opp alt
            var account = new Account();

            // act = gjøre det som skal testes
            account.Deposit(1000);

            // assert = sjekke om det gikk som det skulle
            Assert.That(account.Balance, Is.EqualTo(1000));
        }
    }
}
