using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteCompanyTask.Shared.RepositoryDto
{
    public class AccountsNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AccountNamePaginatedBalanceDto
    {
        public bool HasMore { get; set; }
        public List<AccountsNameDto> Accountslist { get; set; }
    }
    public class AccountDetails{
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string  AccountType { get; set; }
    }
    public class BalanceHistoryDto
    {
        public int? BalanceHistoryId { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? PreviousBalance { get; set; }
        public decimal? FinalBalance { get; set; }
        public decimal? TotalFinal { get; set; }
        public decimal? TotalDebit { get; set; }
        public decimal? TotalCredit { get; set; }
    }
    public class TotalHistoryCount
    {
        public int TotalCount { get; set; }
    }
}