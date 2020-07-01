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
            ViewBag.OptionPG = service.TiposPG();
            if (datosBono == null)
            {
                return HttpNotFound();
            }
            return View(datosBono);
        }

        //GET: DatosBono/Resultados/4
        public ActionResult Results(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoBono resultBono = service.GetResultado((int)id);
            DatosBono datosBono = db.DatosBono.Find(id);
            if (resultBono == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nombre = datosBono.Nombre;
            ViewBag.Moneda = datosBono.Moneda;
            ViewBag.FechaEmision = datosBono.fechaEmision.ToShortDateString();
            return View(resultBono);
        }

        // GET: DatosBono/Create
        public ActionResult Create(int? id)
        {
            
            DatosEmisor datosEmisor = db.DatosEmisor.Find(id);

            ViewBag.IAnual = datosEmisor.Ianual;
            ViewBag.Ir = datosEmisor.Ir;
            ViewBag.Pestructuracion = datosEmisor.Pestructuracion;
            ViewBag.Pcavali = datosEmisor.PCavali;
            ViewBag.Pcolocacion = datosEmisor.Pcolocacion;
            ViewBag.Pflotacion = datosEmisor.Pflotacion;
            ViewBag.Pprima = datosEmisor.Pprima;

            ViewBag.Metodos = service.Metodos();
            ViewBag.TipoAnios = service.TipoAnios();
            ViewBag.Frecuencias = service.Frecuencias();
            ViewBag.TiposTasa = service.TiposTasa();
            ViewBag.TipoMoneda = service.TiposMoneda();
            return View();
        }

        // POST: DatosBono/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Moneda,Metodo,ValNominal,ValComercial,AnosVida,TipoAno,FrecPago,Tea,Tdea,Ianual,Ir,Pprima,Pestructuracion,Pcolocacion,Pflotacion,PCavali,fechaEmision,UserId")] DatosBono datosBono)
        {
            datosBono.UserId = User.Identity.GetUserId();
            int numPeriodos = calculo.NumeroPeriodos(datosBono.TipoAno, datosBono.FrecPago, datosBono.AnosVida);
            if (ModelState.IsValid)
            {
                db.DatosBono.Add(datosBono);
                db.SaveChanges();
                service.CreatePG(datosBono.Id, numPeriodos);
                service.CreateResult(datosBono.Id, true);
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
            ViewBag.TiposTasa = service.TiposTasa();
            return View(datosBono);
        }

        // POST: DatosBono/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Moneda,Metodo,ValNominal,ValComercial,AnosVida,TipoAno,FrecPago,Tea,Tdea,Ianual,Ir,Pprima,Pestructuracion,Pcolocacion,Pflotacion,PCavali,UserId")] DatosBono datosBono)
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
                return RedirectToAction("Details", new { id = datosBono.Id });
            }
            return View(datosBono);
        }

        // GET: DatosBono/EditPG/5
        public ActionResult EditPG(int? id) {
            PGracia pg = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pg = db.PGracia.Find(id);
            if (pg == null)
            {
                return HttpNotFound();
            }
            ViewBag.TiposPg = service.TiposPG();
            return View(pg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPG([Bind(Include = "Id,Tipo,Periodo,DatosBonoId")] PGracia periodoGracia)
        {
            int idCurrentFicha = periodoGracia.DatosBonoId;
            if (ModelState.IsValid)
            {
                service.EditPG(periodoGracia.Id, periodoGracia.Tipo);
                service.CreateResult(idCurrentFicha, false);
                return RedirectToAction("Details", new { id = idCurrentFicha });
            }
            ViewBag.TiposPg = service.TiposPG();
            return View(periodoGracia);
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
            ViewBag.ListPG = service.ListPGracia(datosBono.Id);
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

        public ActionResult EditPGOne(int id) {

            return View("Details");
        }

        
        public ActionResult Type(){
            ViewBag.Types = service.TiposPorcentajes();
            return View();
        }

        [HttpPost]
        public ActionResult Type(FormCollection form)
        {
            string x = form["Alias"];
            int i = Int32.Parse(x);
            return RedirectToAction("Create", routeValues: new { id = i} ) ;
        }
    }
}
