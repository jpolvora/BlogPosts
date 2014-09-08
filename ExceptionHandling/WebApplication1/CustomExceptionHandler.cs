using System;
using System.Diagnostics;
using System.Web;

namespace WebApplication1
{
    public class CustomExceptionHandler : ExceptionHandler<BusinessRuleException>
    {
        public CustomExceptionHandler(HttpApplication application, string errorViewPath)
            : base(application, errorViewPath, LogException)
        {
        }

        static void LogException(Exception exception)
        {
            //send email here...
            Trace.TraceError(exception.Message);
        }

        public override void HandleError()
        {
            base.HandleError();
        }

        protected override bool IsProduction()
        {
            return base.IsProduction();
        }

        protected override void RenderException(BusinessRuleException exception)
        {
            base.RenderException(exception);
        }
    }
}