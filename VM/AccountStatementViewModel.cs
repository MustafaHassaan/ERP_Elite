using Domain.Models;

namespace ERP_Task_Elite.VM
{
    public class AccountStatementViewModel
    {
        public List<TransactionVM> Transactions { get; set; }
        public List<Balance> Accounts { get; set; }
        public int? SelectedAccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        // Paging
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalDebitAll { get; internal set; }
        public decimal TotalCreditAll { get; internal set; }
        public decimal TotalFinalAll { get; internal set; }
        public decimal FirstPrevBalanceAll { get; internal set; }
    }
}
