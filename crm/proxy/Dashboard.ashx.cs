using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace crm.proxy
{
    /// <summary>
    /// Summary description for Dashboard
    /// </summary>
    public class Dashboard : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            switch (context.Request.QueryString["metodo"])
            {
                case "getEtapas":
                    this.getEtapas(context);
                    break;
            }
        }

        public void getEtapas(HttpContext context)
        {
            DataTable response = new DataTable();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}