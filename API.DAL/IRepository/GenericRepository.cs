using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Common;

namespace API.DAL
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        DBEntity context = null;
        private DbSet<T> entities = null;
        System.Data.SqlClient.SqlConnection myConn;
        ErrorLog gobjWriteLog = new ErrorLog();       
        public GenericRepository(DBEntity context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        /// <summary>
        /// Get Data From Database
        /// <para>Use it when to retive data through a stored procedure</para>
        /// </summary>
        public async Task<IEnumerable<T>> ExecuteQuery(string spQuery, string parentMethodName)
        {
            try
            {                
                gobjWriteLog.Error_DataBase(spQuery, parentMethodName, "ExecuteQuery", spQuery);
                return await this.context.Database.SqlQuery<T>(spQuery).ToListAsync();
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_DataBase(spQuery,parentMethodName, "ExecuteQuery", ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Single Data From Database
        /// <para>Use it when to retive single data through a stored procedure</para>
        /// </summary>
        public async Task<T> ExecuteQuerySingle(string spQuery, string parentMethodName)
        {
            try
            {
                gobjWriteLog.Error_DataBase(spQuery,parentMethodName,  "ExecuteQuerySingle", spQuery);
                return await this.context.Database.SqlQuery<T>(spQuery).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_DataBase( spQuery, parentMethodName,"ExecuteQuerySingle", ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Insert/Update/Delete Data To Database
        /// <para>Use it when to Insert/Update/Delete data through a stored procedure</para>
        /// </summary>
        public async Task<int> ExecuteCommand(string spQuery, string parentMethodName)
        {
            int result = 0;
            try
            {
                gobjWriteLog.Error_DataBase(parentMethodName + "_ExecuteCommand", DateTime.Now.ToString(), "DBQuery", spQuery);
                result = await this.context.Database.SqlQuery<int>(spQuery).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_DataBase("ExecuteCommand", parentMethodName + "ExecuteCommand", "", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// ExecuteQuery the user stored procedure with return data as Dataset
        /// <para>To construct the query with sp and parameters</para>
        /// </summary>

        public async Task<DataSet> ExecuteQueryReturnAsDataSet(string spQuery, string parentMethodName)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = await ExeSP(spQuery, parentMethodName);
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_DataBase("ExecuteQueryReturnAsDataSet", parentMethodName + "ExecuteQueryReturnAsDataSet", "", ex.ToString());
            }
            return ds;
        }

        private void GetConnection()
        {
            try
            {
                myConn = new System.Data.SqlClient.SqlConnection(this.context.Database.Connection.ConnectionString);
                myConn.Open();
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_Connection("GetConnection", "GetConnection", ex.ToString());
            }
        }

        private async Task<DataSet> ExeSP(string spQuery, string parentMethodName)
        {
            try
            {
                gobjWriteLog = new ErrorLog(System.Configuration.ConfigurationManager.AppSettings["LogPath"].ToString(), parentMethodName);
                gobjWriteLog.WriteAppLogFiles(spQuery, DateTime.Now, "DBQuery", parentMethodName);
                return await Task<DataSet>.Factory.StartNew(() =>
                {
                    this.GetConnection();
                    DataSet ds = new DataSet();
                    using (System.Data.SqlClient.SqlDataAdapter dt = new System.Data.SqlClient.SqlDataAdapter(spQuery, myConn))
                    {
                        dt.SelectCommand.CommandTimeout = 60;
                        dt.Fill(ds);
                    }                   
                    return ds;
                });
            }
            catch (System.Data.SqlClient.SqlException sqlex)
            {
                gobjWriteLog.Error_DataBase("ExeSP", parentMethodName + "ExeSP","", sqlex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gobjWriteLog.Error_DataBase("ExeSP", parentMethodName + "ExeSP", "", ex.ToString());
                return null;
            }
            finally
            {
                myConn.Close();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
