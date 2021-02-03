using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_API_Demo.Database;
using MISA_API_Demo.Models;

namespace MISA_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {
        protected DBConnector _db;

        /// <summary>
        /// Khởi tạo kết nối tới DB
        /// </summary>
        public BaseEntityController()
        {
            _db = new DBConnector();
        }
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        /// <returns>List bản ghi</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        // GET: api/<CustomersController>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var databaseConnector = new DBConnector();
            var result = databaseConnector.GetAll<TEntity>();
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = result,
                MISACode = EnumCodes.Success,
            });
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="customerId">ID bản ghi</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpGet]
        [Route("{customerId}")]
        public virtual IActionResult Get(Guid customerId)
        {
            var databaseConnector = new DBConnector();
            var result = databaseConnector.GetById<TEntity>(customerId);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = result,
                MISACode = EnumCodes.Success,
            });

        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Kiểu của object</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpPost]
        public virtual IActionResult Post([FromBody] TEntity entity)
        {
            var databaseConnector = new DBConnector();
            var effectRows = databaseConnector.Insert<TEntity>(entity);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = effectRows,
                MISACode = EnumCodes.Success,
            });
        }

        /// <summary>
        /// Chỉnh sửa dữ liệu
        /// </summary>
        /// <param>Bản ghi mới</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpPut]
        public virtual IActionResult Put([FromBody] TEntity entity)
        {
            var databaseConnector = new DBConnector();
            var effectRows = databaseConnector.Update<TEntity>(entity);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = effectRows,
                MISACode = EnumCodes.Success,
            });
        }

        // DELETE api/<CustomersController>/5
        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="customerId">ID của bản ghi</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpDelete]
        [Route("{customerId}")]
        public IActionResult Delete(Guid customerId)
        {
            var databaseConnector = new DBConnector();
            var effectRows = databaseConnector.Delete<TEntity>(customerId);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = effectRows,
                MISACode = EnumCodes.Success,
            });
        }

        // Get api/<startPoint>/<number>
        /// <summary>
        /// Lấy số lượng bản ghi theo khoảng
        /// </summary>
        /// <param name="startPoint">Bản ghi bắt đầu</param>
        /// <param name="number">Số lượng bản ghi cần lấy</param>
        /// <returns>List các bản ghi</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpGet]
        [Route("{startPoint}/{number}")]
        public virtual IActionResult GetWithRange(int startPoint, int number)
        {
            var databaseConnector = new DBConnector();
            var result = databaseConnector.GetWithRange<TEntity>(startPoint, number);
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = result,
                MISACode = EnumCodes.Success,
            });
        }
        //GetMaxCode api/<Employee>/<GetMaxCode>
        /// <summary>
        /// Lấy ra mã lớn nhất trong bảng
        /// </summary>
        /// <returns>Mã lớn nhất</returns>
        /// CreatedBy: TXTrinh (02/02/2021)
        [HttpGet]
        [Route("GetMaxCode")]
        public virtual IActionResult GetMaxCode()
        {
            var databaseConnector = new DBConnector();
            var result = databaseConnector.GetMaxCode<TEntity>();
            return Ok(new ActionServiceResult()
            {
                Success = true,
                Message = "Thành công",
                Data = result,
                MISACode = EnumCodes.Success,
            });
        }
    }
}
