using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr7_trpo_nab.Services
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }
}
