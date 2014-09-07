using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exception()
        {
            throw new Exception("Exception Controller... ");
        }

        public ActionResult BusinessException()
        {
            throw new BusinessRuleException("Business controller ...");
        }

        public ActionResult Status(int status)
        {
            return new HttpStatusCodeResult(status, "Status retornado pelo controller");
        }

        public ActionResult NotFound()
        {
            return HttpNotFound("Status 404 retornado pelo controller");
        }
    }
}