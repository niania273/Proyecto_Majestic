using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Proyecto_Majestic.Controllers
{
    [AuthorizeSessionAtribute]
    public class ProductoController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();

        // GET: Producto
        public ActionResult Index(int pag = 0)
        {
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Marca);

            int c = producto.Count();
            int f = 10;

            int npags = (int)Math.Ceiling((double)c / f);

            ViewBag.pag = pag;
            ViewBag.npags = npags;

            return View(producto.OrderBy(pro => pro.cod_prod).Skip(f * pag).Take(f));
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.cod_cat = new SelectList(db.Categoria, "cod_cat", "nom_cat");
            ViewBag.cod_mar = new SelectList(db.Marca, "cod_mar", "nom_mar");
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_prod,nom_prod,stock_max_prod,stock_min_prod,prec_list_prod,cap_bat,cod_cat,cod_mar,est_prod")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cod_cat = new SelectList(db.Categoria, "cod_cat", "nom_cat", producto.cod_cat);
            ViewBag.cod_mar = new SelectList(db.Marca, "cod_mar", "nom_mar", producto.cod_mar);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.cod_cat = new SelectList(db.Categoria, "cod_cat", "nom_cat", producto.cod_cat);
            ViewBag.cod_mar = new SelectList(db.Marca, "cod_mar", "nom_mar", producto.cod_mar);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_prod,nom_prod,stock_max_prod,stock_min_prod,prec_list_prod,cap_bat,cod_cat,cod_mar,est_prod")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cod_cat = new SelectList(db.Categoria, "cod_cat", "nom_cat", producto.cod_cat);
            ViewBag.cod_mar = new SelectList(db.Marca, "cod_mar", "nom_mar", producto.cod_mar);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            producto.est_prod = false;
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Habilitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            switch (producto.est_prod)
            {
                case true: producto.est_prod = false; break;
                default: producto.est_prod = true; break;
            }
            db.Entry(producto).State = EntityState.Modified;
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
