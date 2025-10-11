using EliteCompanyTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Infrastructure.Helpers
{
    public static class FinalBalanceHelper
    {
     public static  decimal?  ClaculateFinalBalance(string balanceType , BalanceHistory balanceHistory)
        {
            if(balanceType == "D")
            {
                return  
    (balanceHistory.PrevBalnce ?? 0) +
    (balanceHistory.Debtor ?? 0) -
    (balanceHistory.Creditor ?? 0);

            }
            if (balanceType == "C")
            {
                return
    (balanceHistory.PrevBalnce ?? 0) +
    (balanceHistory.Creditor ?? 0) -
    (balanceHistory.Debtor ?? 0);

            }
            return null;

        }
    }
}
