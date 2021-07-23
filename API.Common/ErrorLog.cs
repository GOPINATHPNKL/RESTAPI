using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace API.Common
{
    public class ErrorLog
    {
        #region OLD        
        private string strFileName;
        private string filePathErrLog;
        private string filePathAppLog, gstrCurDir = "";
        public ErrorLog()
        {
            strFileName = System.Configuration.ConfigurationManager.AppSettings["LogPath"].ToString();

            string lstr_Day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
            string lstr_Month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
            gstrCurDir = strFileName + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";
        }

        public ErrorLog(string curDir, string MethodName)
        {
            gstrCurDir = curDir;
            string lstr_Day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
            string lstr_Month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
            string createDir = curDir + @"\Log\" + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";

            if (!Directory.Exists(createDir))
            {
                Directory.CreateDirectory(createDir);
                filePathErrLog = createDir + MethodName + "-";
            }
            else
            {
                filePathErrLog = createDir + MethodName + "-";
            }

            createDir = curDir + @"\AppLog\" + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";
            if (!Directory.Exists(createDir))
            {
                Directory.CreateDirectory(createDir);
                filePathAppLog = createDir + MethodName + "-";
            }
            else
            {
                filePathAppLog = createDir + MethodName + "-";
            }
        }
        public void WriteAppLogFiles(string input, DateTime dt, string ip, string MethodName)
        {
            try
            {
                string CurDate = "", Curtime = "";

                string lstr_Day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
                string lstr_Month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
                string createDir = gstrCurDir + @"\AppLog\" + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";
                if (!Directory.Exists(createDir))
                {
                    Directory.CreateDirectory(createDir);
                    filePathAppLog = createDir + MethodName + "-";
                }
                else
                {
                    filePathAppLog = createDir + MethodName + "-";
                }

                try
                {
                    CurDate = DateTime.Now.ToString("u").Substring(0, 10);
                    Curtime = DateTime.Now.Hour.ToString();
                }
                catch { }
                FileStream fs = new FileStream(filePathAppLog + CurDate + "-" + Curtime + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter w = new StreamWriter(fs);
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.Write(@"""{0}""", System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff:tt"));
                w.WriteLine(@",""" + input + @""" ,""" + ip + @"""");                
                w.Flush();
                w.Close();
                fs.Close();
            }
            catch (IOException IoE)
            {

            }
            catch (Exception ex)
            {

            }
        }

        private string mstrMessage = "";

        public void Error_Connection(string sPathName, string sMethName, string sErrMsg)
        {
            try
            {
                string CurDate = "", Curtime = "";
                string lstr_Day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
                string lstr_Month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
                string createDir = gstrCurDir + @"\Log\" + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";
                if (!Directory.Exists(createDir))
                {
                    Directory.CreateDirectory(createDir);
                    filePathErrLog = createDir + sMethName + "-";
                }
                else
                {
                    filePathErrLog = createDir + sMethName + "-";
                }
                try
                {
                    CurDate = DateTime.Now.ToString("u").Substring(0, 10);
                    Curtime = DateTime.Now.Hour.ToString();
                }
                catch { }
                FileStream fs = new FileStream(filePathErrLog + CurDate + "-" + Curtime + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine("                                 Error Log Details");
                sw.WriteLine(" Date             : {0}                    Time : {1} ", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToLongTimeString().ToString());
                sw.WriteLine(" Form Name        : " + sPathName);
                sw.WriteLine(" Method Name      : " + sMethName);
                sw.WriteLine(" Error Message    : " + sErrMsg);
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine("\n");
                sw.WriteLine("");
                sw.Flush();
                sw.Close();
                mstrMessage = "";
                //FileStream fs = new FileStream(filePathErrLog + CurDate + "-" + Curtime + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //StreamWriter w = new StreamWriter(fs);
                //w.BaseStream.Seek(0, SeekOrigin.End);
                //w.Write(@"""{0}""", System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff:tt"));
                //w.WriteLine(@",""" + sErrMsg + @""" ,""" + sPathName + @"""");
                //w.Flush();
                //w.Close();
                //fs.Close();
            }
            catch (Exception ex)
            {
                mstrMessage = "Unable to Process your Request. Please try Relogin the Application.\nThe cause of the Error is " + ex.ToString();
            }
        }
        public void Error_ClientApp(string sPathName, string sScreenName, string sMethodName, string sErrMsg)
        {
            try
            {
                string CurDate = "", Curtime = "";
                string lstr_Day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
                string lstr_Month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
                string createDir = gstrCurDir + @"\Log\" + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";
                if (!Directory.Exists(createDir))
                {
                    Directory.CreateDirectory(createDir);
                    filePathErrLog = createDir + sMethodName + "-";
                }
                else
                {
                    filePathErrLog = createDir + sMethodName + "-";
                }
                try
                {
                    CurDate = DateTime.Now.ToString("u").Substring(0, 10);
                    Curtime = DateTime.Now.Hour.ToString();
                }
                catch { }
                FileStream fs = new FileStream(filePathErrLog + CurDate + "-" + Curtime + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine("                                 Error Log Details");
                sw.WriteLine(" Date             : {0}                    Time : {1} ", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToLongTimeString().ToString());
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine(" Form Name        : " + sPathName);
                sw.WriteLine(" Screen Name      : " + sScreenName);
                sw.WriteLine(" Method Name      : " + sMethodName);
                sw.WriteLine(" Error Message    : " + sErrMsg);
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine("\n");
                sw.Flush();
                sw.Close();
                mstrMessage = "";
            }
            catch (Exception ex)
            {
                mstrMessage = "Unable to Process your Request. Please try Relogin the Application.\nThe cause of the Error is " + ex.ToString();
            }
        }
        public void Error_DataBase(string sPathName, string sMethName, string sSpName, string sErrMsg)
        {
            try
            {
                string CurDate = "", Curtime = "";
                string lstr_Day = DateTime.Now.Day.ToString().Length == 2 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
                string lstr_Month = DateTime.Now.Month.ToString().Length == 2 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
                string createDir = gstrCurDir + @"\Log\" + DateTime.Now.Year + lstr_Month + lstr_Day + "\\";
                if (!Directory.Exists(createDir))
                {
                    Directory.CreateDirectory(createDir);
                    filePathErrLog = createDir + sMethName + "-";
                }
                else
                {
                    filePathErrLog = createDir + sMethName + "-";
                }
                try
                {
                    CurDate = DateTime.Now.ToString("u").Substring(0, 10);
                    Curtime = DateTime.Now.Hour.ToString();
                }
                catch { }
                StreamWriter sw = new StreamWriter(filePathErrLog + CurDate + "-" + Curtime + ".txt", true);
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine("                                 Error Log Details");
                sw.WriteLine(" Date             : {0}                    Time : {1} ", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToLongTimeString().ToString());
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine(" Form Name        : " + sPathName);
                sw.WriteLine(" Method Name      : " + sMethName);
                sw.WriteLine(" Sp Name          : " + sSpName);
                sw.WriteLine(" Error Message    : " + sErrMsg);
                sw.WriteLine("-----------------------------------------------------------------------------------");
                sw.WriteLine("\n");
                sw.Flush();
                sw.Close();
                mstrMessage = "";
            }
            catch (Exception ex)
            {
                mstrMessage = "Unable to Process your Request. Please try Relogin the Application.\nThe cause of the Error is " + ex.ToString();
            }
        }
        public override string ToString()
        {
            return mstrMessage;
        }
        #endregion        
    }
}
