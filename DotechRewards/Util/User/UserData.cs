using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotechRewards.Util.User
{
    public class UserData
    {
        public static string User
        {
            set
            {
                HttpContext.Current.Session["user"] = value;
            }
            get
            {
                return HttpContext.Current.Session["user"].ToString();
            }
        }

        public static string Admin {
            set {
                HttpContext.Current.Session["admin"] = value;
            }
            get {
                return HttpContext.Current.Session["admin"].ToString(); ;
            }
        }

        public static string Nombre {
            set
            {
                HttpContext.Current.Session["nombre"] = value;
            }
            get
            {
                return HttpContext.Current.Session["nombre"].ToString(); ;
            }
        }

    }

}