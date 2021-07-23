using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Common;
using API.Model;
using API.DAL;
using System.Web.Http.Description;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web.Http.Results;
using System.Text;
using System.Data;

namespace API.Controllers
{
    public class TESTController : ApiController
    {

        /// <summary>
        /// GET MATCH DETAILS
        /// </summary>       
        /// <returns></returns>                
        [ResponseType(typeof(Matchdetails))]
        [HttpGet]
        public async Task<IHttpActionResult> GET()
        {
            try
            {
                IRepositoryTEST s = new TESTService();
                var _DbResponse = await s.GetDbData("get", "getdata");
                if (_DbResponse != null)
                {
                    if (_DbResponse.Tables.Count > 0 && _DbResponse.Tables[0].Rows.Count > 0)
                    {
                        Matchdetails matchdata = new Matchdetails();
                        List<matchinfo> matchinfo = new List<matchinfo>();
                        for (int i = 0; i < _DbResponse.Tables[0].Rows.Count; i++)
                        {
                            matchinfo INFO = new matchinfo();
                            INFO.matchid = Convert.ToInt16(_DbResponse.Tables[0].Rows[i]["matchid"]);
                            INFO.matchdatetime = _DbResponse.Tables[0].Rows[i]["matchdatetime"].ToString();
                            INFO.hometeamname = _DbResponse.Tables[0].Rows[i]["hometeamname"].ToString();
                            INFO.awayteamname = _DbResponse.Tables[0].Rows[i]["awayteamname"].ToString();
                            matchinfo.Add(INFO);
                        }
                        matchdata.matchinfo = matchinfo;
                        return Ok(matchdata);
                    }
                }
                else
                {
                    return InternalServerError();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// UPDATE MATCH DETAILS
        /// </summary>   
        /// <parm name="obj"> matchdatetime format should be 'yyyy-mm-dd hh:mm:ss'
        /// </parm>
        /// <returns></returns>                
        //[ResponseType(typeof())]
        [HttpPost]
        public async Task<IHttpActionResult> UPDATE(updaterq obj)
        {
            BaseResponse objres = null;
            try
            {
                IRepositoryTEST s = new TESTService();
                string _parameters = string.Format("{0},'{1}'", obj.matchid, obj.matchdatetime);
                var _DbResponse = await s.GetDbData(_parameters, "updatedata");

                if (_DbResponse != null)
                {
                    if (_DbResponse.Tables.Count > 0 && _DbResponse.Tables[0].Rows.Count > 0)
                    {
                        objres = new BaseResponse();
                        if (_DbResponse.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            objres.Code = "0";
                            objres.Message = "success";
                        }
                        else
                        {
                            objres.Code = "1";
                            objres.Message = "failed";
                        }

                        return Ok(objres);
                    }
                }
                else
                {
                    return InternalServerError();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// INSERT MATCH DETAILS
        /// </summary>   
        /// <parm name="obj"> matchdatetime format should be 'yyyy-mm-dd hh:mm:ss'
        /// </parm>
        /// <returns></returns>                
        [ResponseType(typeof(Matchdetails))]
        [HttpPost]
        public async Task<IHttpActionResult> INSERT(matchinfo obj)
        {
            BaseResponse objres = null;
            try
            {
                IRepositoryTEST s = new TESTService();
                string _parameters = string.Format("{0},'{1}','{2}','{3}'", obj.matchid, obj.matchdatetime,obj.hometeamname,obj.awayteamname);
                var _DbResponse = await s.GetDbData(_parameters, "postdata");

                if (_DbResponse != null)
                {
                    if (_DbResponse.Tables.Count > 0 && _DbResponse.Tables[0].Rows.Count > 0)
                    {
                        if (_DbResponse.Tables.Count > 0 && _DbResponse.Tables[0].Rows.Count > 0)
                        {
                            Matchdetails matchdata = new Matchdetails();
                            List<matchinfo> matchinfo = new List<matchinfo>();
                            for (int i = 0; i < _DbResponse.Tables[0].Rows.Count; i++)
                            {
                                matchinfo INFO = new matchinfo();
                                INFO.matchid = Convert.ToInt16(_DbResponse.Tables[0].Rows[i]["matchid"]);
                                INFO.matchdatetime = _DbResponse.Tables[0].Rows[i]["matchdatetime"].ToString();
                                INFO.hometeamname = _DbResponse.Tables[0].Rows[i]["hometeamname"].ToString();
                                INFO.awayteamname = _DbResponse.Tables[0].Rows[i]["awayteamname"].ToString();
                                matchinfo.Add(INFO);
                            }
                            matchdata.matchinfo = matchinfo;
                            return Ok(matchdata);
                        }
                    }
                }
                else
                {
                    return InternalServerError();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
