using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BonoLogin.Models;
using Microsoft.AspNet.Identity;
using BonoLogin.Services;

namespace BonoLogin.Controllers
{   [Authorize]
    public class DatosBonoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private BonoService service = new BonoService();
        private CalculoBono calculo = new CalculoBono();
       
        // GET: DatosBono
        public ActionResult Index()
        {
            string idCurrentUser = User.Identity.GetUserId();
            List<DatosBono> lst = db.DatosBono.Where(f => f.UserId == idCurrentUser).ToList();
            if (lst != null) {
                return View(lst);
            }
            else {
                /*Ver si peude poner un mensjae de no tiene registros*/
                return View("Error");
            }
        }

        // GET: DatosBono/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosBono datosBono = db.DatosBono.Find(id);
            ViewBag.ListPG = service.ListPGracia(datosBono.Id);
            if (datosBono == null)
            {
                return HttpNotFound();
            }
            return View(datosBono);
        }

        // GET: DatosBono/Create
        public ActionResult Create()
        {
            ViewBag.Metodos = service.Metodos();
            ViewBag.TipoAnios = service.TipoAnios();
            ViewBag.Frecuencias = service.Frecuencias();
            return View();
        }

        // POST: DatosBono/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Metodo,ValNominal,ValComercial,AnosVida,TipoAno,FrecPago,Tea,Tdea,Ianual,Ir,Pprima,Pestimacion,Pcolocacion,Pflotacion,PCavali,UserId")] DatosBono datosBono)
        {
            datosBono.UserId = User.Identity.GetUserId();
            int numPeriodos = calculo.NumeroPeriodos(datosBono.TipoAno,datosBono.FrecPago,datosBono.AnosVida);
            if (ModelState.IsValid)
            {
                db.DatosBono.Add(datosBono);
                db.SaveChanges();
                service.CreatePG(datosBono.Id, numPeriodos);
                return RedirectToAction("Index");
            }
           return View(datosBono);
        }

        // GET: DatosBono/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosBono datosBono = db.DatosBono.Find(id);
            if (datosBono == null)
            {
                return HttpNotFound();
            }
            ViewBag.Metodos = service.Metodos();
            ViewBag.TipoAnios = service.TipoAnios();
            ViewBag.Frecuencias = service.Frecuencias();
            return View(datosBono);
        }

        // POST: DatosBono/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Metodo,ValNominal,ValComercial,AnosVida,TipoAno,FrecPago,Tea,Tdea,Ianual,Ir,Pprima,Pestimacion,Pcolocacion,Pflotacion,PCavali,UserId")] DatosBono datosBono)
        {
            string idCurrentUser = User.Identity.GetUserId();
            datosBono.UserId = idCurrentUser;
            db.SaveChanges();
            int numPeriodos = calculo.NumeroPeriodos(datosBono.TipoAno, datosBono.FrecPago, datosBono.AnosVida);
            if (ModelState.IsValid)
            {
                db.Entry(datosBono).State = EntityState.Modified;
                db.SaveChanges();
                service.DeletePG(datosBono.Id);
                service.CreatePG(datosBono.Id, numPeriodos);
                return RedirectToAction("Details",new { id = datosBono.Id});
            }
            return View(datosBono);
        }

        // GET: DatosBono/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosBono datosBono = db.DatosBono.Find(id);
            if (datosBono == null)
            {
                return HttpNotFound();
            }
            return View(datosBono);
        }

        // POST: DatosBono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatosBono datosBono = db.DatosBono.Find(id);
            db.DatosBono.Remove(datosBono);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

     }
}
