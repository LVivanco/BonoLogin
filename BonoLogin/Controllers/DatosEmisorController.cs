using BonoLogin.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BonoLogin.Controllers
{   [Authorize(Roles = "Emisor")]
    public class DatosEmisorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DatosEmisor

        public ActionResult Index()
        {
            string idCurrentUser = User.Identity.GetUserId();
            List<DatosEmisor> lst = db.DatosEmisor.Where(de => de.UserId == idCurrentUser).ToList();
            if (lst != null)
            {
                return View(lst);
            }
            else {
                return View("Error");
            }
            
        }

        // GET: DatosEmisor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DatosEmisor datosEmisor = db.DatosEmisor.Find(id);

            if (datosEmisor == null) {
                return HttpNotFound();
            }

            return View(datosEmisor);
        }

        // GET: DatosEmisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DatosEmisor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Alias,Ianual,Ir,Pprima,Pestructuracion,Pcolocacion,Pflotacion,PCavali,UserId")] DatosEmisor datosEmisor)
        {
            datosEmisor.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid) {
                db.DatosEmisor.Add(datosEmisor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datosEmisor);
        }

        // GET: DatosEmisor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DatosEmisor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DatosEmisor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEmisor datosEmisor = db.DatosEmisor.Find(id);
            if (datosEmisor == null) {
                return HttpNotFound();
            }
            return View(datosEmisor);
        }

        // POST: DatosEmisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            DatosEmisor datosEmisor = db.DatosEmisor.Find(id);
            db.DatosEmisor.Remove(datosEmisor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
