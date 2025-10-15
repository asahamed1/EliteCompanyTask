using EliteTask.Application.Models;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.Services
{
    internal static class QueyGenerationHelper
    {
        public static string SelectTotalCountOfTransactionHistory(int balanceId , string from , string to )
        {
            var query = $@"
DECLARE @from VARCHAR(50) = '{from}';
DECLARE @to   VARCHAR(50) = '{to}';
SELECT 
    COUNT(*) AS TotalCount
FROM Balance_History bh
WHERE bh.Balance_ID = {balanceId}
  AND (NULLIF(@from, '') IS NULL OR CAST(bh.[Date] AS DATE) >= TRY_CAST(@from AS DATE))
  AND (NULLIF(@from, '') IS NULL OR CAST(bh.[Date] AS DATE) <= TRY_CAST(@to AS DATE));";
            return query;
        }
        public static string GetAccountHistoryQuery(GetAccountBalanceHistoryModel model , string accountType)
        {
 
            var query = $@"
DECLARE @BalanceType CHAR(1) = '{accountType}';
DECLARE @From VARCHAR(50) = '{model.FromDate}';
DECLARE @To   VARCHAR(50) = '{model.ToDate}';

SELECT 
    Balance_His_ID AS BalanceHistoryId,
    Debtor        AS DebitAmount,
    Creditor      AS CreditAmount,
    Prev_Balnce   AS PreviousBalance,
    CASE 
        WHEN @BalanceType = 'D' THEN (Prev_Balnce + Debtor - Creditor)
        WHEN @BalanceType = 'C' THEN (Prev_Balnce + Creditor - Debtor)
        ELSE 0
    END AS FinalBalance,
    NULL AS TotalPrevious,
    NULL AS TotalDebit,
    NULL AS TotalCredit,
  NULL AS TotalFinal
FROM Balance_History
WHERE Balance_ID = {model.AccountId}
 AND (NULLIF(@From, '') IS NULL OR CAST([Date] AS DATE) >= TRY_CAST(@From AS DATE))
AND (NULLIF(@To, '') IS NULL OR CAST([Date] AS DATE) <= TRY_CAST(@To AS DATE))


UNION ALL

SELECT 
    NULL AS BalanceHistoryId,
    NULL AS DebitAmount,
    NULL AS CreditAmount,
    NULL AS PreviousBalance,
    NULL AS FinalBalance,
    SUM(Prev_Balnce) AS TotalPrevious,
    SUM(Debtor)      AS TotalDebit,
    SUM(Creditor)    AS TotalCredit,
    SUM(
        CASE 
            WHEN @BalanceType = 'D' 
                THEN (Prev_Balnce + Debtor - Creditor)
            WHEN @BalanceType = 'C' 
                THEN (Prev_Balnce + Creditor - Debtor)
            ELSE 0
        END
    ) AS TotalFinal
FROM Balance_History
WHERE Balance_ID = {model.AccountId}
 AND (NULLIF(@From, '') IS NULL OR CAST([Date] AS DATE) >= TRY_CAST(@From AS DATE))
AND (NULLIF(@To, '') IS NULL OR CAST([Date] AS DATE) <= TRY_CAST(@To AS DATE))
";
            return query;
        }
        public static string GetAccountHistoryPaginatedQuery(GetAccountBalanceHistoryModel model, string accountType)
        {
            var offsetValue = (model.PageNumber - 1) * model.PageSize;

            var query = $@"
DECLARE @BalanceType CHAR(1) = '{accountType}';
DECLARE @From VARCHAR(50) = '{model.FromDate}';
DECLARE @To   VARCHAR(50) = '{model.ToDate}';
DECLARE @PageSize INT = {model.PageSize};
DECLARE @OffsetValue INT = {offsetValue};


WITH PaginatedHistory AS
(
    SELECT 
        Balance_His_ID AS BalanceHistoryId,
        Debtor        AS DebitAmount,
        Creditor      AS CreditAmount,
        Prev_Balnce   AS PreviousBalance,
        CASE 
            WHEN @BalanceType = 'D' THEN (Prev_Balnce + Debtor - Creditor)
            WHEN @BalanceType = 'C' THEN (Prev_Balnce + Creditor - Debtor)
            ELSE 0
        END AS FinalBalance,
        NULL AS TotalPrevious,
        NULL AS TotalDebit,
        NULL AS TotalCredit,
        NULL AS TotalFinal
    FROM Balance_History
    WHERE Balance_ID = {model.AccountId}
      AND (NULLIF(@From, '') IS NULL OR CAST([Date] AS DATE) >= TRY_CAST(@From AS DATE))
      AND (NULLIF(@To, '') IS NULL OR CAST([Date] AS DATE) <= TRY_CAST(@To AS DATE))
    ORDER BY [Date]
    OFFSET @OffsetValue ROWS
    FETCH NEXT @PageSize ROWS ONLY
)


SELECT * FROM PaginatedHistory

UNION ALL


SELECT 
    NULL AS BalanceHistoryId,
    NULL AS DebitAmount,
    NULL AS CreditAmount,
    NULL AS PreviousBalance,
    NULL AS FinalBalance,
    SUM(Prev_Balnce) AS TotalPrevious,
    SUM(Debtor)      AS TotalDebit,
    SUM(Creditor)    AS TotalCredit,
    SUM(
        CASE 
            WHEN @BalanceType = 'D' 
                THEN (Prev_Balnce + Debtor - Creditor)
            WHEN @BalanceType = 'C' 
                THEN (Prev_Balnce + Creditor - Debtor)
            ELSE 0
        END
    ) AS TotalFinal
FROM Balance_History
WHERE Balance_ID = {model.AccountId}
  AND (NULLIF(@From, '') IS NULL OR CAST([Date] AS DATE) >= TRY_CAST(@From AS DATE))
  AND (NULLIF(@To, '') IS NULL OR CAST([Date] AS DATE) <= TRY_CAST(@To AS DATE));
";

            return query;
        }
    }
}
