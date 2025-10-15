using EliteCompanyTask.Shared.RepositoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteCompanyTask.Domain.IRepositories
{
    public interface IAccountBalanceHistoryReposiory
    {
        Task<AccountDetails> GetAccount(int accountid);
        Task<AccountNamePaginatedBalanceDto> GetAccountsList(string query, int page = 1, int pageSize = 20);
        Task<List<AccountBalanceHistoryResultDto>> GetAccountHistoryRepository(int? balanceId, string fromDate, string toDate);
        Task<BalancyHistoryDetails> GetBalancyHistory(int id);
    }
}
