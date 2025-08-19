namespace ERP_Task_Elite.VM
{
    public class TransactionVM
    {
        public int? BalanceId { get; set; }
        public int? BalanceHisId { get; set; }
        public string BalanceName { get; set; }
        public decimal? PrevBalnce { get; set; }
        public decimal? Debtor { get; set; }
        public decimal? Creditor { get; set; }
        public string BalanceType { get; set; }
        public decimal FinalBalance { get; set; }
        public DateTime? TranDate { get; set; }
        public string Notes { get; set; }
    }
}
