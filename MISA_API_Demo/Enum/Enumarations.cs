using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_API_Demo.Models
{
    public enum EnumCodes
    {
        /// <summary>
        /// Lỗi dữ liệu
        /// </summary>
        BadRequest = 400,
        Success = 200,
        Exception = 500
    }
    public enum Gender
    {
        Male = 1,
        Female = 0,
        Other = 2
    }
}
