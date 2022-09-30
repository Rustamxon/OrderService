using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; }
        public CustomException(int code, string message) 
            : base(message)
        {
            Code = code;
        }
    }
}
