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
        public async Task<ResultViewModel<AccountBalanceHistoryResultDto>> GetAccountBalanceHistory(GetAccountBalanceHistoryModel model)
        {

            
        var account = await     _accountBalanceHistoryReposiory.GetAccount(model.AccountId.Value);

            var queryOfTotalHistory = QueyGenerationHelper.SelectTotalCountOfTransactionHistory(account.AccountId , model.FromDate , model.ToDate);
            var totalHistoryCount = await _dapperService.QueryAsync<TotalHistoryCount>(queryOfTotalHistory);
            var query =     QueyGenerationHelper.GetAccountHistoryPaginatedQuery(model, account.AccountType);
                 var historyDtos = await  _dapperService.QueryAsync<BalanceHistoryDto>(query);
         var  totals =    historyDtos.LastOrDefault();
            var res = new AccountBalanceHistoryResultDto
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountHistoryList = historyDtos.SkipLast(1),
                TotalCredit = totals?.TotalCredit,
                TotalDebit = totals?.TotalDebit,
                TotalFinal = totals?.TotalFinal,
                TotalCount = totalHistoryCount.FirstOrDefault() != null ? totalHistoryCount.FirstOrDefault().TotalCount:1
            };
            return new ResultViewModel<AccountBalanceHistoryResultDto>()
            {
                Result = res,
                IsSccuss = true
            };
        }

        public async Task<byte[]> ExportBalanceHistory(GetAccountBalanceHistoryModel model)
        {
            
            var account = await _accountBalanceHistoryReposiory.GetAccount(model.AccountId.Value);

            var query = QueyGenerationHelper.GetAccountHistoryQuery(model, account.AccountType);
            var historyDtos = await _dapperService.QueryAsync<BalanceHistoryDto>(query);
            var fileBytes =  _excelExportService.ExportToExcel(account, historyDtos);
           return fileBytes;
        }
        public async Task<byte[]> ExportBalanceHistoryAsPDF(GetAccountBalanceHistoryModel model)
        {
            //};
            var account = await _accountBalanceHistoryReposiory.GetAccount(model.AccountId.Value);

            var query = QueyGenerationHelper.GetAccountHistoryQuery(model, account.AccountType);
            var historyDtos = await _dapperService.QueryAsync<BalanceHistoryDto>(query);
            var fileBytes = _pdfExportService.ExportToPdf(account ,historyDtos);
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
