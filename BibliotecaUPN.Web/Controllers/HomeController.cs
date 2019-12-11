using BibliotecaUPN.Web.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BibliotecaUPN.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var app = new AppContext();
            var model = app.Libros.Include(o => o.Autor).ToList();
            return View(model);
        }       

    }
}