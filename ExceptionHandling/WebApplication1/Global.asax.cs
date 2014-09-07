using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.WebPages;
using System.Web.WebPages.Razor;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

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
            Exception exception = Server.GetLastError();

            HttpException httpException = exception as HttpException
                ?? new HttpException("Unknown exception...", exception);

            Response.StatusCode = httpException.GetHttpCode(); //setar o statuscode para selecionar o template correto no web.config
            Response.StatusDescription = httpException.Message; //msg //todo: editar


            bool producao = ConfigurationManager.AppSettings["Environment"]
                .Equals("Release", StringComparison.OrdinalIgnoreCase);

            if (producao)
            {
                //log or send email to developer notifiying the exception ?
                LogException(httpException);
            }

            if (UseGenericResponse(httpException))
                return;

            //necessário p/ exibir a página customizada
            Server.ClearError(); //deve limpar o erro

            Response.TrySkipIisCustomErrors = true;
            Response.Clear();
            //shows a page with detailed information
            RenderException(Context, exception);

            Response.End();
        }

        private static bool UseGenericResponse(HttpException exception)
        {
            var code = exception.GetHttpCode();
            if (code == 404)
                return true;

            return exception.InnerException is HttpException;

        }

        static void LogException(HttpException exception)
        {
            //send email here...
        }

        static void RenderException(HttpContext httpContext, Exception exception)
        {
            try
            {
                //atualiza os paths para o request (exceto RawUrl)
                httpContext.RewritePath("~/error.cshtml");

                var handler = WebPageHttpHandler.CreateFromVirtualPath("~/Error.cshtml");
                httpContext.Session["exception"] = exception;
                handler.ProcessRequest(httpContext);
                httpContext.Session.Remove("exception");
            }
            catch
            {
                //erro ao renderizar a página customizada, então fallback p/ response.write()
                httpContext.Response.Write(exception.ToString());
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}