namespace ERP_Task_Elite.VM
{
    public class TransactionDetailsVM
    {
        public int BalanceHisId { get; set; }
        public decimal? PrevBalnce { get; set; }
        public decimal? Debtor { get; set; }
        public decimal? Creditor { get; set; }
        public DateTime TranDate { get; set; }
        public string Notes { get; set; }
    }
}
