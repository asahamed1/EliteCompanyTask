using EliteCompanyTask.Shared.RepositoryDto;
using EliteTask.Application.IServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection.Metadata;
using static iTextSharp.text.pdf.AcroFields;

namespace EliteTask.Application.Services
{
    public class PdfExportService: IPdfExportService
    {
        public byte[] ExportToPdf(AccountDetails
         account, IEnumerable<BalanceHistoryDto> data)
        {
            using var memoryStream = new MemoryStream();
            var document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Title
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            document.Add(new Paragraph("Account Balance Report", titleFont));
            document.Add(new Paragraph(" ")); // Empty line

            // Table with 3 columns (customize as needed)
          
            PdfPTable table = new PdfPTable(6);
            table.AddCell("Account ID");
            table.AddCell("Account Name");
            table.AddCell("Debit Amount");
            table.AddCell("Credit Amount");
            table.AddCell("Previous Balance");

            table.AddCell("Final Balance");
           
          

            foreach (var item in data)
            {
                table.AddCell(account.AccountId.ToString());
                table.AddCell(account.AccountName);
                table.AddCell(item.DebitAmount.ToString());
                table.AddCell(item.CreditAmount.ToString());
                table.AddCell(item.PreviousBalance.ToString());
                table.AddCell(item.FinalBalance.ToString());
               
            }
            
            document.Add(new Paragraph("Total Debit", titleFont));
            document.Add(new Paragraph(data.LastOrDefault()?.TotalDebit.ToString())); // Empty line
            document.Add(new Paragraph("Total Credit", titleFont));
            document.Add(new Paragraph(data.LastOrDefault()?.TotalCredit.ToString())); // Empty line
            document.Add(new Paragraph("Total Final Balance", titleFont));
            document.Add(new Paragraph(data.LastOrDefault()?.FinalBalance.ToString())); // Empty line

            document.Add(table);


            document.Close();

            return memoryStream.ToArray();
        }
    }
}
