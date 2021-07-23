using API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using API.Common;
using System.Threading.Tasks;

namespace API.DAL
{
    public class TESTService : IRepositoryTEST
    {
        ErrorLog gobjWriteLog = new ErrorLog();
        public async Task<DataSet> GetDbData(string parameters, string MethodName)
        {
            try
            {
                string lstrMethodName = string.Empty;
                string spQuery = string.Empty;
                switch (MethodName)
                {
                    case "getdata":
                        lstrMethodName = EnumStoreProcedure.getdata;
                        spQuery = string.Format("{0}", lstrMethodName);
                        break;
                    case "postdata":
                        lstrMethodName = EnumStoreProcedure.postdata;
                        spQuery = string.Format("{0} {1}", lstrMethodName, parameters);
                        break;
                    case "updatedata":
                        lstrMethodName = EnumStoreProcedure.updatedata;
                        spQuery = string.Format("{0} {1}", lstrMethodName, parameters);
                        break;
                    case "deletedata":
                        lstrMethodName = EnumStoreProcedure.deletedata;
                        spQuery = string.Format("{0} {1}", lstrMethodName, parameters);
                        break;
                }
                
                using (var CustRepository = new GenericRepository<BaseResponse>(new DBEntity()))
                {
                    return await CustRepository.ExecuteQueryReturnAsDataSet(spQuery, MethodName);
                }
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_DataBase(MethodName, MethodName, parameters, ex.ToString());
                return null;
            }
        }
    }
}
