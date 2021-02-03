using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_API_Demo.Models;
using MISA_API_Demo.Services;

namespace MISA_API_Demo.Controllers
{
    public class EmployeesController : BaseEntityController<Employee>
    {
        /// <summary>
        /// Lấy tất cả bản ghi và sắp xếp
        /// </summary>
        /// <returns>Response tương ứng cho Client</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        public override IActionResult Get()
        {
            var sql = "Select * from Employee ORDER BY EmployeeCode ASC";
            var employees = _db.GetAll<Employee>(sql);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = employees,
                MISACode = EnumCodes.Success,
            });
        }
        /// <summary>
        /// Tạo mới nhân viên
        /// </summary>
        /// <param name="employee">Thực thể nhân viên mới</param>
        /// <returns>Response tương ứng cho Client</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        public override IActionResult Post([FromBody] Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();

            EmployeeService employeeService = new EmployeeService();
            var res = employeeService.InsertEmployee(employee);
            switch (res.MISACode)
            {
                case EnumCodes.Success:
                    return Ok(res);
                case EnumCodes.BadRequest:
                    return BadRequest(res);
                case EnumCodes.Exception:
                    return StatusCode(500);
                default:
                    return NoContent();
            }
        }
        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thực thể đã sửa</param>
        /// <returns>Response tương ứng cho Client</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        public override IActionResult Put([FromBody] Employee employee)
        {
            EmployeeService employeeService = new EmployeeService();
            var res = employeeService.UpdateEmployee(employee);
            switch (res.MISACode)
            {
                case EnumCodes.Success:
                    return Ok(res);
                case EnumCodes.BadRequest:
                    return BadRequest(res);
                case EnumCodes.Exception:
                    return StatusCode(500);
                default:
                    return NoContent();
            }
        }
        /// <summary>
        /// Tìm kiếm bản ghi theo điều kiện
        /// </summary>
        /// <param name="EmployeeCode">Mã nhân viên</param>
        /// <param name="Position">Vị trí</param>
        /// <param name="Department">Phòng ban</param>
        /// <returns>List bản ghi</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string EmployeeCode, string Position, string Department)
        {
            string sql;
            if ((EmployeeCode.ToString() != "no") && (Position.ToString() == "no") && (Department.ToString() == "no")) sql = $"SELECT * FROM Employee WHERE EmployeeCode LIKE '%{EmployeeCode.ToString()}%'";
            else if ((EmployeeCode.ToString() == "no") && (Position.ToString() != "no") && (Department.ToString() == "no")) sql = $"SELECT * FROM Employee WHERE PositionId = '{Position}'";
            else if ((EmployeeCode.ToString() == "no") && (Position.ToString() == "no") && (Department.ToString() != "no")) sql = $"SELECT * FROM Employee WHERE DepartmentId = '{Department}'";
            else if ((EmployeeCode.ToString() != "no") && (Position.ToString() != "no") && (Department.ToString() == "no")) sql = $"SELECT * FROM Employee WHERE EmployeeCode LIKE '%{EmployeeCode.ToString()}%' AND PositionId = '{Position}'";
            else if ((EmployeeCode.ToString() != "no") && (Position.ToString() == "no") && (Department.ToString() != "no")) sql = $"SELECT * FROM Employee WHERE EmployeeCode LIKE '%{EmployeeCode.ToString()}%' AND DepartmentId = '{Department}'";
            else if ((EmployeeCode.ToString() == "no") && (Position.ToString() != "no") && (Department.ToString() != "no")) sql = $"SELECT * FROM Employee WHERE PositionId = '{Position}' AND DepartmentId = '{Department}'";
            else if ((EmployeeCode.ToString() != "no") && (Position.ToString() != "no") && (Department.ToString() != "no")) sql = $"SELECT * FROM Employee WHERE EmployeeCode LIKE '%{EmployeeCode.ToString()}%' AND PositionId = '{Position}' AND DepartmentId = '{Department}'";
            else sql = $"Select * from Employee ORDER BY EmployeeCode ASC";
            var employees = _db.GetAll<Employee>(sql);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = employees,
                MISACode = EnumCodes.Success,
            });
        }
    }
}
