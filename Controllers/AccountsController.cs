using ClosedXML.Excel;
using Domain.Models;
using ERP_Task_Elite.VM;
using Microsoft.AspNetCore.Mvc;
using Reositories.Interfaces;
using System.Drawing.Printing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERP_Task_Elite.Controllers
{
    public class AccountsController : Basecontroller
    {
        public AccountsController(IUnitofwork IUW) : base(IUW) { }

        public async Task<IActionResult> Index(int accountId, DateTime? fromDate, DateTime? toDate, int page = 1, int pageSize = 10)
        {
            var vm = new AccountStatementViewModel
            {
                Accounts = await _IUW.Balances.GetAllAsync(),
                SelectedAccountId = accountId,
                FromDate = fromDate ?? new DateTime(DateTime.Now.Year, 1, 1),
                ToDate = toDate ?? DateTime.Now
            };
            if (accountId > 0)
            {
                var balances = await _IUW.Balances.GetAllAsync();
                var history = await _IUW.BalanceHistory.GetAllAsync();

                var transactions = (from b in balances
                                    join bh in history
                                         on b.BalanceId equals bh.BalanceId into gj
                                    from bh in gj.DefaultIfEmpty()
                                    where b.BalanceId == accountId
                                          && (!fromDate.HasValue || bh.Date >= fromDate.Value)
                                          && (!toDate.HasValue || bh.Date <= toDate.Value)
                                    select new TransactionVM
                                    {
                                        BalanceId = b.BalanceId,
                                        BalanceHisId = Convert.ToInt32(bh?.BalanceHisId),
                                        BalanceName = b.BalanceName,
                                        PrevBalnce = bh?.PrevBalnce,
                                        Debtor = bh?.Debtor,
                                        Creditor = bh?.Creditor,
                                        BalanceType = b.BalanceType,
                                        FinalBalance = b.BalanceType == "D"
                                            ? (bh?.PrevBalnce ?? 0) + (bh?.Debtor ?? 0) - (bh?.Creditor ?? 0)
                                            : (bh?.PrevBalnce ?? 0) + (bh?.Creditor ?? 0) - (bh?.Debtor ?? 0)
                                    }).ToList();

                //vm.Transactions = transactions;
                // احسب الإجماليات الكلية قبل الباجينيشن
                vm.TotalDebitAll = transactions.Sum(t => t.Debtor ?? 0);
                vm.TotalCreditAll = transactions.Sum(t => t.Creditor ?? 0);
                vm.TotalFinalAll = transactions.Sum(t => t.FinalBalance);
                vm.FirstPrevBalanceAll = transactions.Sum(t => t.PrevBalnce ?? 0);
                // إجمالي عدد السجلات
                vm.TotalRecords = transactions.Count();
                vm.TotalPages = (int)Math.Ceiling((double)vm.TotalRecords / vm.PageSize);

                // نجيب الصفحة المطلوبة بس
                var totalCount = transactions.Count;
                vm.TotalCount = totalCount;
                vm.Transactions = transactions
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                vm.AccountName = vm.Accounts.FirstOrDefault(x => x.BalanceId == accountId)?.BalanceName;
                vm.AccountType = vm.Accounts.FirstOrDefault(x => x.BalanceId == accountId)?.BalanceType;

            }
            accountId = 0;
            vm.PageNumber = page;
            vm.PageSize = pageSize;
            return View(vm);
        }
        public async Task<IActionResult> GetTransactionDetails(int id)
        {
            var history = await _IUW.BalanceHistory.FindAsync(x => x.BalanceHisId == id);

            if (history == null)
                return NotFound();

            var vm = new TransactionVM
            {
                BalanceHisId = (int?)history.BalanceHisId,
                BalanceId = history.BalanceId,
                PrevBalnce = history.PrevBalnce,
                Debtor = history.Debtor,
                Creditor = history.Creditor,
                TranDate = history.Date,
                Notes = history.Remarks
            };

            return PartialView("_TransactionDetails", vm);
        }
        public async Task<IActionResult> ExportToExcel(int accountId, DateTime? fromDate, DateTime? toDate)
        {
            var balances = await _IUW.Balances.GetAllAsync();
            var history = await _IUW.BalanceHistory.GetAllAsync();

            var transactions = (from b in balances
                                join bh in history
                                     on b.BalanceId equals bh.BalanceId into gj
                                from bh in gj.DefaultIfEmpty()
                                where b.BalanceId == accountId
                                      && (!fromDate.HasValue || bh.Date >= fromDate.Value)
                                      && (!toDate.HasValue || bh.Date <= toDate.Value)
                                select new TransactionVM
                                {
                                    BalanceId = b.BalanceId,
                                    BalanceName = b.BalanceName,
                                    PrevBalnce = bh?.PrevBalnce,
                                    Debtor = bh?.Debtor,
                                    Creditor = bh?.Creditor,
                                    BalanceType = b.BalanceType,
                                    FinalBalance = b.BalanceType == "D"
                                        ? (bh?.PrevBalnce ?? 0) + (bh?.Debtor ?? 0) - (bh?.Creditor ?? 0)
                                        : (bh?.PrevBalnce ?? 0) + (bh?.Creditor ?? 0) - (bh?.Debtor ?? 0)
                                }).ToList();

            if (transactions == null || !transactions.Any())
                return BadRequest("No data to export");
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Transactions");
                ws.Cell(1, 1).Value = "Account ID";
                ws.Cell(1, 2).Value = "Account Name";
                ws.Cell(1, 3).Value = "Previous Balance";
                ws.Cell(1, 4).Value = "Debit Amount";
                ws.Cell(1, 5).Value = "Credit Amount";
                ws.Cell(1, 6).Value = "Final Balance";

                // إضافة البيانات من الـ List
                for (int i = 0; i < transactions.Count; i++)
                {
                    ws.Cell(i + 2, 1).Value = transactions[i].BalanceId;
                    ws.Cell(i + 2, 2).Value = transactions[i].BalanceName;
                    ws.Cell(i + 2, 3).Value = transactions[i].PrevBalnce;
                    ws.Cell(i + 2, 4).Value = transactions[i].Debtor;
                    ws.Cell(i + 2, 5).Value = transactions[i].Creditor;
                    ws.Cell(i + 2, 6).Value = transactions[i].FinalBalance;
                }

                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    return File(memoryStream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Account Statement(Full Period Transactions).xlsx");
                }
            }
        }
    }

}
