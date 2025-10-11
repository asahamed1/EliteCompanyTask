using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteCompanyTask.Shared.RepositoryDto
{
    public class AccountsNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AccountNamePaginatedBalanceDto
    {
        public bool HasMore { get; set; }
        public List<AccountsNameDto>Accountslist { get; set; }
    }
}