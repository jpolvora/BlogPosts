using System;
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
            using (var handler = new ExceptionHandler<BusinessRuleException>(this, LogException))
            {
                //checa se o ambiente é de produção
                bool releaseMode = ConfigurationManager.AppSettings["Environment"]
                    .Equals("Release", StringComparison.OrdinalIgnoreCase);

                handler.Handle(releaseMode);
            }
        }

        static void LogException(Exception exception)
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