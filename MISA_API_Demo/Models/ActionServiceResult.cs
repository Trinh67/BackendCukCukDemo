using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_API_Demo.Models
{ 
    /// <summary>
    /// Mẫu dữ liệu trả về
    /// </summary>
    public class ActionServiceResult
    {
        /// <summary>
        /// Trạng thái trả về
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Thông báo trả về
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Mã Code trả về
        /// </summary>
        public EnumCodes MISACode { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
    }
}
