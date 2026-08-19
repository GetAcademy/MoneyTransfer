namespace MoneyTransfer.OnlyAPI.DTO
{
    public class TransferMoneyRequest
    {
        public string FromAccountNo { get; set; } = "";
        public string ToAccountNo { get; set; } = "";
        public decimal Amount { get; set; }
    }
}
