using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;

namespace Proyecto_Majestic.Controllers
{
    [AuthorizeSessionAtribute]
    public class MarcaController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();

        // GET: Marca
        public ActionResult Index(int pag = 0)
        {
            var marca = db.Marca.ToList();

            int c = marca.Count();
            int f = 10;

            int npags = (int)Math.Ceiling((double)c / f);

            ViewBag.pag = pag;
            ViewBag.npags = npags;

            return View(marca.OrderBy(pe => pe.cod_mar).Skip(f * pag).Take(f));
        }

        // GET: Marca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // GET: Marca/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_mar,nom_mar,est_mar")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Marca.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: Marca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marca/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_mar,nom_mar,est_mar")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }

        // GET: Marca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            marca.est_mar = false;
            db.Entry(marca).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Habilitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            switch (marca.est_mar)
            {
                case true: marca.est_mar = false; break;
                default: marca.est_mar = true; break;
            }
            db.Entry(marca).State = EntityState.Modified;
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
