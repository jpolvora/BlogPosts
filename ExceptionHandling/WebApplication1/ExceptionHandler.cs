using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace WebApplication1
{
    public class ExceptionHandler<TException> : IDisposable
        where TException : Exception
    {
        private readonly HttpApplication _application;
        private readonly Action<Exception> _log;

        public ExceptionHandler(HttpApplication application, bool relase)
            : this(application, exception => Trace.TraceError(exception.Message))
        {

        }

        public ExceptionHandler(HttpApplication application, Action<Exception> log)
        {
            this._application = application;
            _log = log;
        }

       
        public void Handle(bool releaseMode)
        {
            var server = _application.Server;
            var response = _application.Response;
            var context = _application.Context;
            var session = _application.Session;

            Exception ex = server.GetLastError();

            HttpException httpException = ex as HttpException ?? new HttpException("Unknown exception...", ex);

            var rootException = httpException.GetBaseException();

            if (releaseMode)
            {
                //log or send email to developer notifiying the exception ?
                _log(httpException);
                server.ClearError();
            }

            var statusCode = httpException.GetHttpCode();

            //setar o statuscode para que o IIS selecione a view correta (no web.config)
            response.StatusCode = statusCode;
            response.StatusDescription = rootException.Message; //todo: colocar uma msg melhor


            switch (statusCode)
            {
                case 404:
                    break;
                case 500:
                    {
                        //check for exception types you want to show custom info
                        //for example, business rules exceptions
                        if (!(rootException is TException))
                        {
                            //will show default 500.
                            //to show default 500 in dev mode, call Server.ClearError() 
                            //or modify web.config to debug=false
                            break;
                        }
                        server.ClearError();
                        response.TrySkipIisCustomErrors = true;
                        response.Clear();

                        try
                        {
                            //atualiza os paths para o request (exceto RawUrl)
                            context.RewritePath("~/error.cshtml");

                            var handler = WebPageHttpHandler.CreateFromVirtualPath("~/Error.cshtml");
                            session["exception"] = rootException;
                            handler.ProcessRequest(context);
                            session.Remove("exception");
                        }
                        catch
                        {
                            //erro ao renderizar a página customizada, então Response.Write como fallback
                            response.Write(rootException.ToString());
                        }
                        break;
                    }
            }
        }

        public void Dispose()
        {
            _application.CompleteRequest();
        }
    }
}