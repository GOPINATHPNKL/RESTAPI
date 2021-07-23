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
    public class QPService : IRepositoryQP
    {
        ErrorLog gobjWriteLog = new ErrorLog();
        public async Task<DataSet> GetDbData(string parameters, string MethodName)
        {
            try
            {
                string lstrMethodName = string.Empty;
                switch (MethodName)
                {
                    case "QPBuyInitiate":
                        lstrMethodName = EnumStoreProcedure.QPBuyInitiate;
                        break;
                    case "Buy":
                        lstrMethodName = EnumStoreProcedure.QuickPickBuyConfirm;
                        break;
                    case "TransactionReversal":
                        lstrMethodName = EnumStoreProcedure.QuickPickBuyCancel;
                        break;
                    case "QPCheckClaim":
                        lstrMethodName = EnumStoreProcedure.QuickPickCheckClaim;
                        break;
                    case "QPPrizeClaim":
                        lstrMethodName = EnumStoreProcedure.QuickPickPrizeClaim;
                        break;
                    case "ClaimReversal":
                        lstrMethodName = EnumStoreProcedure.QuickPickClaimCancel;
                        break;
                }
                string spQuery = string.Format("{0} {1}", lstrMethodName, parameters);
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
