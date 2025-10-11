using Azure;
using EliteCompanyTask.Domain.Entities;
using EliteCompanyTask.Domain.IRepositories;
using EliteCompanyTask.Shared;
using EliteCompanyTask.Shared.RepositoryDto;
using EliteTask.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EliteTask.Infrastructure.Repositories
{
    public class AccountBalanceHistoryReposiory : IAccountBalanceHistoryReposiory
    {
        private readonly ApplicationDbContext _context;

        public AccountBalanceHistoryReposiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountBalanceHistoryResultDto>> GetAccountHistoryRepository(int? balanceId, string fromDate, string toDate)
        {
            var balanceIdParam = new Microsoft.Data.SqlClient.SqlParameter("@BalanceId", balanceId ?? (object)DBNull.Value);
            var fromDateParam = new Microsoft.Data.SqlClient.SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value);
            var toDateParam = new Microsoft.Data.SqlClient.SqlParameter("@ToDate", toDate ?? (object)DBNull.Value);

            return await _context.Set<AccountBalanceHistoryResultDto>()
                .FromSqlRaw("EXEC dbo.GetAccountBalanceSummary @BalanceId, @FromDate, @ToDate",
                            balanceIdParam, fromDateParam, toDateParam)
                .AsNoTracking().ToListAsync();
        }

        public async Task<AccountNamePaginatedBalanceDto> GetAccountsList(string query, int page = 1, int pageSize = 20)
        {
            var skip = (page - 1) * pageSize;
            string key = "";
            string balanceName = "";
            if (!String.IsNullOrEmpty(query) && query.Contains("-"))
            {
                var splitResult = query.Split("-");
                key = splitResult[0];
                balanceName = splitResult[1];
            }
            var usersQuery = _context.Balances
                .WhereIf(!String.IsNullOrEmpty(query) && !query.Contains("-"), u => u.ParentCode.ToString().Contains(query) || u.BalanceNameEn.Contains(query) )
                .WhereIf(!String.IsNullOrEmpty(query) && query.Contains("-"), u => u.ParentCode.ToString().Contains(key) && u.BalanceNameEn.Contains(balanceName))
                ;

            var totalCount = usersQuery.Count();

            var balanceList = usersQuery
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new AccountsNameDto { Id = u.BalanceId, Name = u.ParentCode + "-"+ u.BalanceNameEn })
                .ToList();

            var hasMore = totalCount > skip + pageSize;

            
            return new AccountNamePaginatedBalanceDto
            {
                Accountslist = balanceList,
                HasMore = hasMore,
            };
        }

        public async Task<BalancyHistoryDetails> GetBalancyHistory(int id)
        {
            return await _context.BalanceHistories.Where(x => x.BalanceHisId == id).Select(x => new BalancyHistoryDetails
            {
                Creditor = x.Debtor,
                Debtor = x.Debtor,
                Remarks = x.Remarks,
                NewBalance = x.NewBalnce,
                PreviousBalance = x.PrevBalnce,
                Date = x.Date == null ? null : x.Date.Value.ToDateTimeString()
            }).FirstOrDefaultAsync();
        }
    }
}
