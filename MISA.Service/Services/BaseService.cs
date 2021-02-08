using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Models;
using MISA.DataLayer;

namespace MISA.Service
{
    class BaseService<TEntity>
    {
        DBConnector<TEntity> _dBConnector;
        ActionServiceResult _actionServiceResult;
        string className = typeof(TEntity).Name;

        public ActionServiceResult GetData()
        {
            _dBConnector = new DBConnector<TEntity>();
            _actionServiceResult = new ActionServiceResult();
            var data = _dBConnector.GetAll();
            return _actionServiceResult;
        }
    }
}
