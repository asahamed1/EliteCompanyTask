using AutoMapper;
using EliteCompanyTask.Domain.Entities;
using EliteCompanyTask.Domain.IRepositories;
using EliteCompanyTask.Shared.RepositoryDto;
using EliteTask.Application.Dtos;
using EliteTask.Application.IServices;
using EliteTask.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IAccountBalanceHistoryReposiory _accountBalanceHistoryReposiory;
        private readonly IDapperService _dapperService;     
        private readonly IExcelExportService _excelExportService;
        private readonly IPdfExportService _pdfExportService;
        public BalanceService( IPdfExportService pdfExportService, IExcelExportService excelExportService, IDapperService dapperService, IAccountBalanceHistoryReposiory accountBalanceHistoryReposiory)
        {
            _accountBalanceHistoryReposiory = accountBalanceHistoryReposiory;  
            _dapperService = dapperService; 
            _excelExportService = excelExportService;
            _pdfExportService = pdfExportService;
        }

       

        public async Task<ResultViewModel<AccountNamePaginatedBalanceDto>> GetAllBalances(string query, int page = 1, int pageSize = 20)
        {
         var balanceList =   await  _accountBalanceHistoryReposiory.GetAccountsList(query , page , pageSize);

            return new ResultViewModel<AccountNamePaginatedBalanceDto>
            {
                IsSccuss = true,
                Result = balanceList
            };

        }
        public async Task<ResultViewModel<IEnumerable<AccountBalanceHistoryResultDto>>> GetAccountBalanceHistory(GetAccountBalanceHistoryModel model)
        {
           
            //var history = await _accountBalanceHistoryReposiory.GetAccountHistoryRepository(model.AccountId , model.FromDate , model.ToDate);
            //return new ResultViewModel<List<AccountBalanceHistoryResultDto>>()
            //{
            //    Result = history,
            //    IsSccuss = true
            //};
         var res = await  _dapperService.QueryAsync<AccountBalanceHistoryResultDto>("GetAccountBalanceSummary", new { BalanceId = model.AccountId, From = model.FromDate, To = model.ToDate });
            return new ResultViewModel<IEnumerable<AccountBalanceHistoryResultDto>>()
            {
                Result = res,
                IsSccuss = true
            };
        }

        public async Task<byte[]> ExportBalanceHistory(GetAccountBalanceHistoryModel model)
        {
            var res = await _dapperService.QueryAsync<AccountBalanceHistoryResultDto>("GetAccountBalanceSummary", new { BalanceId = model.AccountId, From = model.FromDate, To = model.ToDate });
           var fileBytes =  _excelExportService.ExportToExcel(res);
           return fileBytes;
        }
        public async Task<byte[]> ExportBalanceHistoryAsPDF(GetAccountBalanceHistoryModel model)
        {
            var res = await _dapperService.QueryAsync<AccountBalanceHistoryResultDto>("GetAccountBalanceSummary", new { BalanceId = model.AccountId, From = model.FromDate, To = model.ToDate });
            var fileBytes = _pdfExportService.ExportToPdf(res);
            return fileBytes;
        }

        public async Task<ResultViewModel<BalancyHistoryDetails>> GetHistoryDetails(int id)
        {
         var history =   await  _accountBalanceHistoryReposiory.GetBalancyHistory(id);
            return new ResultViewModel<BalancyHistoryDetails>
            {
                IsSccuss = true,
                Result = history
            };
        }
    }
}
