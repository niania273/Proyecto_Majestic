using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;

namespace Proyecto_Majestic.Controllers
{
    [AuthorizeSessionAtribute]
    public class ClienteController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();

        // GET: Cliente
        public ActionResult Index(int pag = 0)
        {
            var cliente = db.Cliente.Include(cli => cli.Distrito);

            int c = cliente.Count();
            int f = 10;

            int npags = (int)Math.Ceiling((double)c / f);

            ViewBag.pag = pag;
            ViewBag.npags = npags;

            return View(cliente.OrderBy(cli => cli.cod_cli).Skip(f * pag).Take(f));
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ViewBag.cod_dis = new SelectList(db.Distrito, "cod_dis", "nom_dis");
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_cli,ruc_cli,nom_cli,telf_cli,email_cli,dir_cli,cod_dis,est_cli")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cod_dis = new SelectList(db.Distrito, "cod_dis", "nom_dis", cliente.cod_dis);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.cod_dis = new SelectList(db.Distrito, "cod_dis", "nom_dis", cliente.cod_dis);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_cli,ruc_cli,nom_cli,telf_cli,email_cli,dir_cli,cod_dis,est_cli")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cod_dis = new SelectList(db.Distrito, "cod_dis", "nom_dis", cliente.cod_dis);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            cliente.est_cli = false;
            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Habilitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            switch (cliente.est_cli)
            {
                case true: cliente.est_cli = false; break;
                default: cliente.est_cli = true; break;
            }
            db.Entry(cliente).State = EntityState.Modified;
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
