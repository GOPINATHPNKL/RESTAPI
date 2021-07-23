using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Common;

namespace API.DAL
{
    interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> ExecuteQuery(string spQuery, string parentMethodName);
        Task<T> ExecuteQuerySingle(string spQuery, string parentMethodName);
        Task<int> ExecuteCommand(string spQuery, string parentMethodName);
        Task<DataSet> ExecuteQueryReturnAsDataSet(string spQuery, string parentMethodName);
    }
}
