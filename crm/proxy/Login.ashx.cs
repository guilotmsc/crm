using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistencia;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using System.Web.Helpers;

namespace crm.proxy
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            switch (context.Request.QueryString["metodo"])
            {
                case "logar":
                    login(context);
                    break;
            }
        }
         
        public void login(HttpContext context)
        {
            User user = new User(); 
            DataTable response;
            user.usuario = context.Request.Form["usuario"];
            user.senha = context.Request.Form["senha"];
            String teste = String.Empty;
            try
            {
                response = user.login(user);

                if (Convert.ToInt32(response.Rows[0]["count"]) < 1)
                {
                    user.usuario = null;
                    user.senha = null;

                    throw new Exception();
                } else
                {
                    context.Response.Write("{{\"message\": \"action was successful!\"}}");
                }
                 
            }
            catch (Exception)
            {
                throw;
            }
            

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