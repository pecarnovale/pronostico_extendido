using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PronosticoExtendido.Controllers
{

    public class PronosticoController : Controller
    {
        public PartialViewResult RenderZona()
        {
            return PartialView("Zona");
        }

        public PartialViewResult RenderPronostico()
        {
            return PartialView("Pronostico");
        }

        public PartialViewResult RenderExtendido()
        {
            return PartialView("Extendido");
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}