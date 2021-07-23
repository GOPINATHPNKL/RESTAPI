using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using API.Model;
using API.Common;
using System.Data.Entity;
using System.Text;
using System.Net;

namespace API.DAL
{
    public partial class DBEntity : DbContext
    {
        //clsBusinessComponent BCL = new clsBusinessComponent();
        ErrorLog Errlog = new ErrorLog();
        public DBEntity()
            : base("name=APIConnection")
        {
            this.Database.CommandTimeout = 30;
        }
                   
    }
}
