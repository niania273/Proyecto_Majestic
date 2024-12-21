using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Proyecto_Majestic.Controllers
{
    [AuthorizeSessionAtribute]
    public class EmpleadoController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();

        List<SelectListItem> listaSexos = new List<SelectListItem>
        {
            new SelectListItem { Text = "Femenino", Value = "F" },
            new SelectListItem { Text = "Masculino", Value = "M" }
        };

        // GET: Empleado
        public ActionResult Index(int pag = 0)
        {
            var empleado = db.Empleado;

            int c = empleado.Count();
            int f = 10;

            int npags = (int)Math.Ceiling((double)c / f);

            ViewBag.pag = pag;
            ViewBag.npags = npags;

            return View(empleado.OrderBy(pe => pe.cod_emp).Skip(f * pag).Take(f));
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.lstSexos = new SelectList(listaSexos, "Value", "Text");
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_emp,nom_emp,ape_emp,dni_emp,fecha_in_emp,telf_emp,sexo_emp,num_emp,est_emp")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                ViewBag.lstSexos = new SelectList(listaSexos, "Value", "Text", empleado.sexo_emp);
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.lstSexos = new SelectList(listaSexos, "Value", "Text", empleado.sexo_emp);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_emp,nom_emp,ape_emp,dni_emp,fecha_in_emp,telf_emp,sexo_emp,num_emp,est_emp")] Empleado empleado)
        {
            ViewBag.lstSexos = new SelectList(listaSexos, "Value", "Text", empleado.sexo_emp);
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            empleado.est_emp = false;
            db.Entry(empleado).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Habilitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            switch (empleado.est_emp)
            {
                case true: empleado.est_emp = false; break;
                default: empleado.est_emp = true; break;
            }
            db.Entry(empleado).State = EntityState.Modified;
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
