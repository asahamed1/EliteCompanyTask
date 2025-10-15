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
        public string AccountName { get; set; }
         public IEnumerable<BalanceHistoryDto> AccountHistoryList { get; set; }
        public decimal? TotalFinal { get; set; }
        public decimal? TotalDebit { get; set; }
        public decimal? TotalCredit { get; set; }
        public int TotalCount { get; set; }
    }
}
