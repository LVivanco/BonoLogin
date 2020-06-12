using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BonoLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descripción del equipo";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Información del equipo";

            return View();
        }
    }
}