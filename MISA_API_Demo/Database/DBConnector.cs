using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace MISA_API_Demo.Database
{
    public class DBConnector
    {
        public static string connectionString = "Host=103.124.92.43;Port=3306; User Id=nvmanh;Password=12345678;Database=MS2_30_Trinh_CukCuk;Character Set=utf8";
        
        IDbConnection _db;
        public DBConnector()
        {
            _db = new MySqlConnection(connectionString);
        }
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll<TEntity>()
        {
            string className = typeof(TEntity).Name;
            var sql = $"SELECT * FROM {className}";
            var entities = _db.Query<TEntity>(sql);
            return entities;
        }
        /// <summary>
        /// Lấy danh sách theo commandText
        /// </summary>
        /// <typeparam name="TEntity">Kiểu của object</typeparam>
        /// <returns>Mảng các object lấy đc</returns>
        public IEnumerable<TEntity> GetAll<TEntity>(string commandText)
        {
            string className = typeof(TEntity).Name;
            var sql = commandText;
            var entities = _db.Query<TEntity>(sql);
            return entities;
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        /// <typeparam name="TEntity">Loại đối tượng</typeparam>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>một đối tượng</returns>
        public TEntity GetById<TEntity>(object id)
        {
            string className = typeof(TEntity).Name;
            var sql = $"Select * from {className} Where {className}Id = '{id.ToString()}'";
            return _db.Query<TEntity>(sql).FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới thông tin
        /// </summary>
        /// <typeparam name="TEntity">Loại đối tượng</typeparam>
        /// <param name="entity">Dối tượng mới</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        public int Insert<TEntity>(TEntity entity)
        {
            string className = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties();
            var parameters = new DynamicParameters();
            var sqlPropertyBuilder = string.Empty;
            var sqlParamBuilder = string.Empty;
            foreach(var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                parameters.Add($"@{propertyName}", propertyValue);
                sqlPropertyBuilder += $",{propertyName}";
                sqlParamBuilder += $",@{propertyName}";
            }
        
            var sql = $"INSERT INTO {className}({sqlPropertyBuilder.Substring(1)}) VALUE({sqlParamBuilder.Substring(1)})";
            var effectRows = _db.Execute(sql, parameters);
            return effectRows;
        }
        /// <summary>
        /// Chỉnh sửa thông tin
        /// </summary>
        /// <typeparam name="TEntity">Loại đối tượng</typeparam>
        /// <param name="entity">Đối tượng mới </param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        public int Update<TEntity>(TEntity entity)
        {
            string className = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties();
            var parameters = new DynamicParameters();
            var sqlBuilder = string.Empty;
            var sqlParamBuilder = string.Empty;
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                parameters.Add($"@{propertyName}", propertyValue);
                sqlBuilder += $",{propertyName} = @{propertyName}";
            }
            sqlBuilder = sqlBuilder.Substring(1);
            int position = sqlBuilder.IndexOf(",");
            var sqlCompare = sqlBuilder.Substring(0, position);
            sqlBuilder = sqlBuilder.Substring(position + 1);
            var sql = $"UPDATE {className} SET {sqlBuilder} WHERE {sqlCompare}";
            var effectRows = _db.Execute(sql, parameters);
            return effectRows;
        }
        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <typeparam name="TEntity">Loại đối tượng</typeparam>
        /// <param name="id">Id của đối tượng cần xóa</param>
        /// <returns>Số bản ghi ảnh hường</returns>
        public int Delete<TEntity>(object id)
        {
            string className = typeof(TEntity).Name;
            var sql = $"DELETE FROM {className} WHERE {className}Id = '{id.ToString()}'";
            return _db.Execute(sql);
        }
        /// <summary>
        /// Lấy bản ghi từ vị trí, số lượng lấy
        /// </summary>
        /// <typeparam name="TEntity">Loại đối tượng</typeparam>
        /// <param name="startPoint">Thứ tự bản ghi bắt đầu lấy</param>
        /// <param name="number">Số lượng bản ghi cần lấy</param>
        /// <returns>Mảng các đối tượng</returns>
        public IEnumerable<TEntity> GetWithRange<TEntity>(int startPoint, int number)
        {
            string className = typeof(TEntity).Name;
            var sql = $"SELECT * FROM {className}  ORDER BY {className}Code ASC LIMIT {startPoint}, {number}";
            var entities = _db.Query<TEntity>(sql);
            return entities;
        }
    }
}
