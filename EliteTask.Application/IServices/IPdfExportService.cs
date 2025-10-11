using EliteCompanyTask.Shared.RepositoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.IServices
{
    public interface IPdfExportService
    {
     public byte[] ExportToPdf(IEnumerable<AccountBalanceHistoryResultDto> data);
    }
}
