using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteCompanyTask.Shared.RepositoryDto;

using System.ComponentModel;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using EliteTask.Application.IServices;

namespace EliteTask.Application.Services
{
  
    public class ExcelExportService:IExcelExportService
    {
        public byte[] ExportToExcel(AccountDetails
         account, IEnumerable<BalanceHistoryDto> data)
        { 
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Account History");

                // Add Header Row
                sheet.Cells[1, 1].Value = "Account ID";
                sheet.Cells[1, 2].Value = "Account Name";
                sheet.Cells[1, 3].Value = "Debit Amount";
                sheet.Cells[1, 4].Value = "Credit Amount";
                sheet.Cells[1, 5].Value = "Previous Balance";
                sheet.Cells[1, 6].Value = "Final Balance";
                sheet.Cells[1, 10].Value = "Total Debit";
                sheet.Cells[1, 11].Value = "Total Credit";
                sheet.Cells[1, 12].Value = "Total Final Balance";

                using (var range = sheet.Cells[1, 1, 1, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Add Data Rows
                int row = 2;
                foreach (var item in data)
                {
                    sheet.Cells[row, 1].Value = account.AccountId;
                    sheet.Cells[row, 2].Value = account.AccountName;
                    sheet.Cells[row, 3].Value = item.DebitAmount;
                    sheet.Cells[row, 4].Value = item.CreditAmount;
                    sheet.Cells[row, 5].Value = item.PreviousBalance;
                    sheet.Cells[row, 6].Value = item.FinalBalance;
                    row++;
                }
                sheet.Cells[2, 10].Value = data.LastOrDefault()?.TotalDebit;
                sheet.Cells[2, 11].Value = data.LastOrDefault()?.TotalCredit;
                sheet.Cells[2, 12].Value = data.LastOrDefault()?.TotalFinal;
                // Auto-fit columns
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }

}
