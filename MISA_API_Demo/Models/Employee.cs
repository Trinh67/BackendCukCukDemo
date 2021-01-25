using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_API_Demo.Models
{
    public class Employee
    {
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public Guid EmployeeId { get; set; }
        [Required("Mã nhân viên", "Bạn phải nhập mã Nhân viên")]
        [CheckDuplicate("Mã nhân viên", "Mã nhân viên đã tồn tại!")]
        [MaxLength("Mã nhân viên", 20)]
        public string EmployeeCode { get; set; }
        [Required("Họ Tên Nhân viên", "Bạn phải nhập tên NV")]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        [Required("Mã thẻ căn cước", "Bạn phải nhập mã thẻ căn cước")]
        [CheckDuplicate("Mã thẻ căn cước", "Mã thẻ căn cước đã tồn tại!")]
        [MaxLength("Mã thẻ căn cước", 20)]
        public string IdentifyNumber { get; set; }
        public DateTime? DateOfIn { get; set; }
        public string PlaceOfIn { get; set; }
        [Required("Địa chỉ email", "Bạn phải email Nhân viên")]
        public string Email { get; set; }
        [Required("Số điện thoại nhân viên", "Bạn phải nhập sđt NV")]
        [CheckDuplicate("Số điện thoại nhân viên", "Số điện thoại đã tồn tại!")]
        public string PhoneNumber { get; set; }
        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public string TaxCode { get; set; }
        public string Salary { get; set; }
        public DateTime? DateOfBeginWork { get; set; }
        public int? Status { get; set; }
    }
}
