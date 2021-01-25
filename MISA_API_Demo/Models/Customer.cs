using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_API_Demo.Models
{
    public class Customer
    {
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }
        [Required("Mã khách hàng", "Bạn phải nhập mã KH")]
        [CheckDuplicate("Mã khách hàng", "Mã khách hàng đã tồn tại!")]
        [MaxLength("Mã khách hàng", 20)]
        public string CustomerCode { get; set; }
        [Required("Họ Tên khách hàng", "Bạn phải nhập tên KH")]
        public string FullName { get; set; }
        public string MemberCard { get; set; }
        public Guid CustomerGroupID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        [Required("Số điện thoại khách hàng", "Bạn phải nhập sđt KH")]
        [CheckDuplicate("Số điện thoại khách hàng", "Số điện thoại đã tồn tại!")]
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTaxCode { get; set; }
        public string Address { get; set; }
    }
}
