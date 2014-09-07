﻿using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapRoute("default", "{controller}/{action}/{id}", new
            {
                controller = "home",
                action = "index",
                id = UrlParameter.Optional
            });

            //GlobalFilters.Filters.Add(new DebugHandleError());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            HttpException httpException = ex as HttpException ?? new HttpException("Unknown exception...", ex);

            var rootException = httpException.GetBaseException();

            //checa se o ambiente é de produção
            bool release = ConfigurationManager.AppSettings["Environment"]
                .Equals("Release", StringComparison.OrdinalIgnoreCase);

            if (release)
            {
                //log or send email to developer notifiying the exception ?
                LogException(httpException);
                Server.ClearError();
            }

            var statusCode = httpException.GetHttpCode();

            //setar o statuscode para que o IIS selecione a view correta (no web.config)
            Response.StatusCode = statusCode;
            Response.StatusDescription = rootException.Message; //todo: colocar uma msg melhor

            
            switch (statusCode)
            {
                case 404:
                    break;
                case 500:
                    {
                        //check for exception types you want to show custom info
                        //for example, business rules exceptions
                        if (!(rootException is BusinessRuleException))
                        {
                            //will show default 500.
                            //to show default 500 in dev mode, call Server.ClearError() 
                            //or modify web.config to debug=false
                            break;
                        }
                        Server.ClearError();
                        Response.TrySkipIisCustomErrors = true;
                        Response.Clear();

                        try
                        {
                            //atualiza os paths para o request (exceto RawUrl)
                            Context.RewritePath("~/error.cshtml");

                            var handler = WebPageHttpHandler.CreateFromVirtualPath("~/Error.cshtml");
                            Session["exception"] = rootException;
                            handler.ProcessRequest(Context);
                            Session.Remove("exception");
                        }
                        catch
                        {
                            //erro ao renderizar a página customizada, então Response.Write como fallback
                            Response.Write(httpException.ToString());
                        }
                        break;
                    }
            }
        }

        static void LogException(HttpException exception)
        {
            //send email here...
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}