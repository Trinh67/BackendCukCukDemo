using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_API_Demo.Models
{
    public class MISAAttribute
    {
    }
    /// <summary>
    /// Attribute bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required:Attribute
    {
        /// <summary>
        /// Tên property
        /// </summary>
        public string PropertyName;
        /// <summary>
        /// Thong báo gặp lỗi
        /// </summary>
        public string ErrorMsg;
        public Required(string propertyName, string errorMsg = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMsg = errorMsg;
        }
    }
    /// <summary>
    /// Attribute check trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {
        /// <summary>
        /// Tên property
        /// </summary>
        public string PropertyName;
        /// <summary>
        /// Thong báo gặp lỗi
        /// </summary>
        public string ErrorMsg;
        public CheckDuplicate(string propertyName, string errorMsg = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMsg = errorMsg;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        /// <summary>
        /// Tên property
        /// </summary>
        public string PropertyName;
        /// <summary>
        /// Thong báo gặp lỗi
        /// </summary>
        public string ErrorMsg;
        /// <summary>
        /// Độ dài tối đa
        /// </summary>
        public int Length { get; set; }
        public MaxLength(string propertyName, int length, string errorMsg = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMsg = errorMsg;
            Length = length;
        }
    }
}
