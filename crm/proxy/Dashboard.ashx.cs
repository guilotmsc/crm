using System;
using System.Collections.Generic;
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
            context.Response.Write("Hello World");
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