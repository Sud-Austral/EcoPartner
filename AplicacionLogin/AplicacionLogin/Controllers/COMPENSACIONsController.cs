using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicacionLogin.Models;

namespace AplicacionLogin.Controllers
{
    public class COMPENSACIONsController : Controller
    {
        private ecopartnerEntities db = new ecopartnerEntities();

        // GET: COMPENSACIONs
        public ActionResult Index()
        {
            return View(db.COMPENSACION.ToList());
        }

        // GET: COMPENSACIONs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPENSACION cOMPENSACION = db.COMPENSACION.Find(id);
            if (cOMPENSACION == null)
            {
                return HttpNotFound();
            }
            return View(cOMPENSACION);
        }

        // GET: COMPENSACIONs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: COMPENSACIONs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,telefono,nombreEmpresa,pais,mail,toneladas,compensacion1,id_codigo")] COMPENSACION cOMPENSACION)
        {
            if (ModelState.IsValid)
            {
                db.COMPENSACION.Add(cOMPENSACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMPENSACION);
        }

        // GET: COMPENSACIONs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPENSACION cOMPENSACION = db.COMPENSACION.Find(id);
            if (cOMPENSACION == null)
            {
                return HttpNotFound();
            }
            return View(cOMPENSACION);
        }

        // POST: COMPENSACIONs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,telefono,nombreEmpresa,pais,mail,toneladas,compensacion1,id_codigo")] COMPENSACION cOMPENSACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMPENSACION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMPENSACION);
        }

        // GET: COMPENSACIONs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPENSACION cOMPENSACION = db.COMPENSACION.Find(id);
            if (cOMPENSACION == null)
            {
                return HttpNotFound();
            }
            return View(cOMPENSACION);
        }

        // POST: COMPENSACIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            COMPENSACION cOMPENSACION = db.COMPENSACION.Find(id);
            db.COMPENSACION.Remove(cOMPENSACION);
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
