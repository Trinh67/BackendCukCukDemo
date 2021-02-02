using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA_API_Demo.Database;
using MISA_API_Demo.Models;
using MISA_API_Demo.Services;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA_API_Demo.Controllers
{
    public class CustomersController : BaseEntityController<Customer>
    {
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        public override IActionResult Get()
        {
            var sql = "Select * from Customer LIMIT 10";
            var customers = _db.GetAll<Customer>(sql);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = customers,
                MISACode = EnumCodes.Success,
            });
        }
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Khách hàng mới</param>
        /// <returns></returns>
        public override IActionResult Post([FromBody] Customer customer)
        {
            customer.CustomerId = Guid.NewGuid();

            CustomerService customerService = new CustomerService();
            var res = customerService.InsertCustomer(customer);
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
        /// Sửa khách hàng
        /// </summary>
        /// <param name="customer">Khách hàng đã sửa</param>
        /// <returns></returns>
        public override IActionResult Put([FromBody] Customer customer)
        {
            CustomerService customerService = new CustomerService();
            var res = customerService.UpdateCustomer(customer);
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
    }
}
