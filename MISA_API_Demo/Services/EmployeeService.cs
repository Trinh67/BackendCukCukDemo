using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA_API_Demo.Database;
using MISA_API_Demo.Models;

namespace MISA_API_Demo.Services
{
    public class EmployeeService
    {
        DBConnector _dBConnector;
        ActionServiceResult _actionServiceResult;
        public EmployeeService()
        {
            _dBConnector = new DBConnector();
            _actionServiceResult = new ActionServiceResult();
        }
        public ActionServiceResult InsertEmployee(Employee employee)
        {
            // Validate
            ValidateObj(employee, 0);
            if (_actionServiceResult.MISACode == EnumCodes.BadRequest)
            {
                return _actionServiceResult;
            }

            return new ActionServiceResult()
            {
                Success = true,
                Message = Properties.Resources.Msg_Success,
                Data = _dBConnector.Insert<Employee>(employee),
                MISACode = EnumCodes.Success,
            };
        }
        public ActionServiceResult UpdateEmployee(Employee employee)
        {
            // Validate
            ValidateObj(employee, 1);
            if (_actionServiceResult.MISACode == EnumCodes.BadRequest)
            {
                return _actionServiceResult;
            }

            return new ActionServiceResult()
            {
                Success = true,
                Message = Properties.Resources.Msg_Success,
                Data = _dBConnector.Update<Employee>(employee),
                MISACode = EnumCodes.Success,
            };
        }

        private void ValidateObj(Employee employee, int index)
        {
            var properties = typeof(Employee).GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(employee);
                // Nếu có attribute là [Required] thì kiểm tra
                if (property.IsDefined(typeof(Required), true) && (propValue == null || propValue.ToString() == string.Empty))
                {
                    var requiredAttribute = property.GetCustomAttributes(typeof(Required), true).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        var propertyText = (requiredAttribute as Required).PropertyName;
                        _actionServiceResult.Message += $"{propertyText} không được để trống. ";
                    }
                    _actionServiceResult.MISACode = EnumCodes.BadRequest;
                }
                // Nếu có attribute là [CheckDuplicate] thì kiểm tra
                if (property.IsDefined(typeof(CheckDuplicate), true) && (index == 0))
                {
                    var requiredAttribute = property.GetCustomAttributes(typeof(CheckDuplicate), true).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        var propertyText = (requiredAttribute as CheckDuplicate).PropertyName;
                        var sql = $"Select {propName} From {typeof(Employee).Name} Where {propName} = '{propValue}'";
                        var entity = _dBConnector.GetAll<Employee>(sql).FirstOrDefault();
                        if (entity != null)
                        {
                            _actionServiceResult.Message += $"{propertyText} đã tồn tại rồi nhé! ";
                            _actionServiceResult.MISACode = EnumCodes.BadRequest;
                        }
                    }
                }
                // Nếu có attribute là [MaxLength] thì kiểm tra
                if (property.IsDefined(typeof(MaxLength), true))
                {
                    var requiredAttribute = property.GetCustomAttributes(typeof(MaxLength), true).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        var propertyText = (requiredAttribute as MaxLength).PropertyName;
                        var length = (requiredAttribute as MaxLength).Length;
                        if (propValue.ToString().Trim().Length > length)
                        {
                            _actionServiceResult.Message += $"{propertyText} dài quá quy định! ";
                            _actionServiceResult.MISACode = EnumCodes.BadRequest;
                        }
                    }
                }
            }
        }
    }
}
