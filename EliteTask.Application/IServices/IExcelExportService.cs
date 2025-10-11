using EliteCompanyTask.Shared.RepositoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.IServices
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel(IEnumerable<AccountBalanceHistoryResultDto> data);
    }
}
