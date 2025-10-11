using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteCompanyTask.Domain.IRepositories
{
    
        public interface IDapperService
        {
            Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
            Task<dynamic> ExecuteAsync(string sql, object? param = null);
        }

        
    

}
