using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class TEST
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class Matchdetails
    {
        public List<matchinfo> matchinfo { get; set; }     
    }
    public class matchinfo
    {      
        public int matchid { get; set; }
        public string hometeamname { get; set; }
        public string awayteamname { get; set; }
        public string matchdatetime { get; set; }
    }
    public class updaterq
    {
        [Required]
        public int matchid { get; set; }
        [Required]
        public string matchdatetime { get; set; }
    }
    public class BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
