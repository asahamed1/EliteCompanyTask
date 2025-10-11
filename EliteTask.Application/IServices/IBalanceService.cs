using EliteCompanyTask.Shared.RepositoryDto;
using EliteTask.Application.Dtos;
using EliteTask.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.IServices
{
    public interface IBalanceService
    {
        public  Task <ResultViewModel<AccountNamePaginatedBalanceDto>> GetAllBalances(string query, int page = 1, int pageSize = 20);
        public  Task<ResultViewModel<IEnumerable<AccountBalanceHistoryResultDto>>> GetAccountBalanceHistory(GetAccountBalanceHistoryModel model);
        public Task<byte[]> ExportBalanceHistory(GetAccountBalanceHistoryModel model);
        public Task<byte[]> ExportBalanceHistoryAsPDF(GetAccountBalanceHistoryModel model);
        public Task<ResultViewModel<BalancyHistoryDetails>> GetHistoryDetails(int id);



    }
}
 