using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.Models
{
    public class GetAccountBalanceHistoryModel
    {
        [Required(ErrorMessage = "Please select an account")]
        public int? AccountId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
