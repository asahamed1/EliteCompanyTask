using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteCompanyTask.Shared.RepositoryDto
{
   
    public class AccountBalanceHistoryResultDto
    {
        public int AccountId { get; set; }
        public int BalanceHistoryId { get; set; }
        public string AccountName { get; set; }
        public decimal? PreviousBalance { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? FinalBalance { get; set; }
        public decimal? TotalDebit { get; set; }
        public decimal? Totalprevious { get; set; }
        public decimal? TotalCredit { get; set; }
        public decimal? TotalFinalBalance{ get; set; }

     //   public IEnumerable<AccountBlanceHistoryDto> AccountHistoryList { get; set; }
    }
}
