using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Proyecto_Majestic.Controllers
{
    [AuthorizeSessionAtribute]
    public class CategoriaController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();

        // GET: Categoria
        public ActionResult Index(int pag = 0)
        {
            var categoria = db.Categoria.ToList();

            int c = categoria.Count();
            int f = 10;

            int npags = (int)Math.Ceiling((double)c / f);

            ViewBag.pag = pag;
            ViewBag.npags = npags;

            return View(categoria.OrderBy(ca => ca.cod_cat).Skip(f * pag).Take(f));
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_cat,nom_cat,est_cat")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categoria.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_cat,nom_cat,est_cat")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            categoria.est_cat = false;
            db.Entry(categoria).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Habilitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            switch (categoria.est_cat)
            {
                case true: categoria.est_cat = false; break;
                default: categoria.est_cat = true; break;
            }
            db.Entry(categoria).State = EntityState.Modified;
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
