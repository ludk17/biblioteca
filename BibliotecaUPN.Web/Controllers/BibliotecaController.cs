using BibliotecaUPN.Web.Constantes;
using BibliotecaUPN.Web.DB;
using BibliotecaUPN.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BibliotecaUPN.Web.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var app = new AppContext(); 
            Usuario user = (Usuario)Session["Usuario"];

            var model = app.Bibliotecas
                .Include(o => o.Libro.Autor)
                .Include(o=> o.Usuario)
                .Where(o => o.UsuarioId == user.Id)
                .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int libro)
        {
            var app = new AppContext();
            Usuario user = (Usuario)Session["Usuario"];

            // TO-DO validar si ya existe el libro en la biblioteca, en ese caso no guardar y notificar

            var biblioteca = new Biblioteca {
                LibroId = libro,
                UsuarioId = user.Id,
                Estado = ESTADO.POR_LEER
            };

            app.Bibliotecas.Add(biblioteca);
            app.SaveChanges();

            TempData["SuccessMessage"] = "Se añádio el libro a su biblioteca";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MarcarComoLeyendo(int libroId)
        {
            var app = new AppContext();
            Usuario user = (Usuario)Session["Usuario"];

            // TO-DO validar si ya existe el libro en la biblioteca, en ese caso no guardar y notificar

            var libro = app.Bibliotecas
                .Where(o => o.LibroId == libroId && o.UsuarioId == user.Id)
                .FirstOrDefault();

            libro.Estado = ESTADO.LEYENDO;
            app.SaveChanges();            

            TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarcarComoTerminado(int libroId)
        {
            var app = new AppContext();
            Usuario user = (Usuario)Session["Usuario"];

            // TO-DO validar si ya existe el libro en la biblioteca, en ese caso no guardar y notificar

            var libro = app.Bibliotecas
                .Where(o => o.LibroId == libroId && o.UsuarioId == user.Id)
                .FirstOrDefault();

            libro.Estado = ESTADO.TERMINADO;
            app.SaveChanges();

            TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }
    }
}