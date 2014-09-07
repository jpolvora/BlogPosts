using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

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

        public ActionResult Status(int? status)
        {
            Response.SetStatus(status.GetValueOrDefault());
            return View("Index");
        }

        public ActionResult NotFound()
        {
            return HttpNotFound("Status 404 retornado pelo controller");
        }
    }
}