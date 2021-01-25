using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_API_Demo.Models
{ 
    public class ActionServiceResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public EnumCodes MISACode { get; set; }

        public object Data { get; set; }
    }
}
