using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public interface IRepositoryTEST
    {
        Task<DataSet> GetDbData(string parameters, string MethodName);
    }
}
