using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteCompanyTask.Shared.RepositoryDto
{
    public class BalancyHistoryDetails
    {
        public decimal? Creditor { get; set;}
        public decimal? Debtor { get; set; }

        public decimal? PreviousBalance { get; set; }
        public decimal? NewBalance { get; set; }
        public string Remarks { get; set; }
        public string Date  { get; set; }
    }
}
