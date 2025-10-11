using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTask.Application.Dtos
{
    public class ResultViewModel<T>
    {
        public T Result { get; set; }   
        public bool IsSccuss { get; set; }
    }
}
