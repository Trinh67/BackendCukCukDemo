﻿using System;
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
        public override IActionResult Get()
        {
            var sql = "Select * from Employee";
            var employees = _db.GetAll<Employee>(sql);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = employees,
                //MISACode = "OK",
            });
        }
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
        [HttpGet]
        [Route("{EmployeeCode}/{Position}/{Department}")]
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
            else sql = $"SELECT * FROM Employee";
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