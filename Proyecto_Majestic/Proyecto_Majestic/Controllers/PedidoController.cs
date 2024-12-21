using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Proyecto_Majestic.Controllers
{
    [AuthorizeSessionAtribute]
    public class PedidoController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();

        List<SelectListItem> lstActualizacion = new List<SelectListItem>
        {
            new SelectListItem { Text = "Entregado", Value = "Entregado" },
            new SelectListItem { Text = "En Proceso", Value = "En Proceso" },
            new SelectListItem { Text = "Pagado", Value = "Pagado" },
            new SelectListItem { Text = "Cancelado", Value = "Cancelado" },
            new SelectListItem { Text = "Devuelto", Value = "Devuelto" },
            new SelectListItem { Text = "Enviado", Value = "Enviado" }
        };

        // GET: Pedido
        public ActionResult Index(int pag = 0)
        {
            var pedido = db.Pedido.Include(p => p.Cliente).Include(p => p.Empleado).Include(p => p.Pedido_Producto.Select(pp => pp.Producto));

            int c = pedido.Count();
            int f = 10;

            int npags = (int)Math.Ceiling((double)c / f);

            ViewBag.pag = pag;
            ViewBag.npags = npags;

            return View(pedido.OrderBy(pe => pe.cod_ped).Skip(f * pag).Take(f));
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pedido = db.Pedido
               .Include(p => p.Pedido_Producto.Select(pp => pp.Producto))
               .FirstOrDefault(p => p.cod_ped == id.Value);

            pedido.Pedido_Producto = pedido.Pedido_Producto
                    .Where(pp => pp.est_pp == true)
                    .ToList();

            ViewBag.cod_cli = new SelectList(db.Cliente, "cod_cli", "nom_cli", pedido.cod_cli);
            ViewBag.cod_emp = new SelectList(db.Empleado, "cod_emp", "nom_emp", pedido.cod_emp);
            return View(pedido);

        }

        // GET: Pedido/Create
        public ActionResult Create()
        {
            var empleados = db.Empleado
                      .Select(e => new
                      {
                          e.cod_emp,
                          nom_completo = e.nom_emp + " " + e.ape_emp
                      })
                      .ToList();

            ViewBag.cod_cli = new SelectList(db.Cliente, "cod_cli", "nom_cli");
            ViewBag.cod_emp = new SelectList(empleados, "cod_emp", "nom_completo");
            ViewBag.lstActualizacion = new SelectList(lstActualizacion, "Text", "Value");
            ViewBag.lstProductos = new SelectList(db.Producto, "cod_prod", "nom_prod");
            return View();
        }

        // POST: Pedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_ped,cod_cli,cod_emp,fec_ped,act_ped,est_ped")] Pedido pedido, int[] Productos, int[] Cantidades)
        {
            if (ModelState.IsValid)
            {
                ViewBag.lstActualizacion = new SelectList(lstActualizacion, "Text", "Value", pedido.act_ped);
                db.Pedido.Add(pedido);

                for (int i = 0; i < Productos.Length; i++)
                {
                    int codProducto = Productos[i];
                    Producto producto = db.Producto.FirstOrDefault(p => p.cod_prod == codProducto);

                    if (producto != null)
                    {
                            pedido.Pedido_Producto.Add(new Pedido_Producto
                            {
                                cod_prod = producto.cod_prod,
                                pre_prod = producto.prec_list_prod,
                                can_prod = Cantidades[i],
                                est_pp = true
                            });
                        }
                    }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var empleados = db.Empleado
                      .Select(e => new
                      {
                          e.cod_emp,
                          nom_completo = e.nom_emp + " " + e.ape_emp
                      })
                      .ToList();


            ViewBag.cod_cli = new SelectList(db.Cliente, "cod_cli", "nom_cli", pedido.cod_cli);
            ViewBag.cod_emp = new SelectList(empleados, "cod_emp", "nom_completo", pedido.cod_emp);
            ViewBag.lstProductos = new SelectList(db.Producto, "cod_prod", "nom_prod");
            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pedido = db.Pedido
               .Include(p => p.Pedido_Producto.Select(pp => pp.Producto))
               .FirstOrDefault(p => p.cod_ped == id.Value);

            pedido.Pedido_Producto = pedido.Pedido_Producto
                    .Where(pp => pp.est_pp == true)
                    .ToList();

            var empleados = db.Empleado
                      .Select(e => new
                      {
                          e.cod_emp,
                          nom_completo = e.nom_emp + " " + e.ape_emp
                      })
                      .ToList();

            ViewBag.cod_cli = new SelectList(db.Cliente, "cod_cli", "nom_cli", pedido.cod_cli);
            ViewBag.cod_emp = new SelectList(empleados, "cod_emp", "nom_completo", pedido.cod_emp);
            ViewBag.lstActualizacion = new SelectList(lstActualizacion, "Value", "Text", pedido.act_ped);
            ViewBag.lstProductos = new SelectList(db.Producto, "cod_prod", "nom_prod");
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_ped,cod_cli,cod_emp,fec_ped,act_ped,est_ped")] Pedido pedido, int[] Productos, int[] Cantidades)
        {
            var empleados = db.Empleado
                      .Select(e => new
                      {
                          e.cod_emp,
                          nom_completo = e.nom_emp + " " + e.ape_emp
                      })
                      .ToList();

            ViewBag.lstActualizacion = new SelectList(lstActualizacion, "Text", "Value", pedido.act_ped);

            if (ModelState.IsValid)
            {
                //Buscar pedido
                var pedidoDb = db.Pedido.Include(p => p.Pedido_Producto).FirstOrDefault(p => p.cod_ped == pedido.cod_ped);

                //Actualizar pedido
                if (pedidoDb != null)
                {
                    pedidoDb.cod_cli = pedido.cod_cli;
                    pedidoDb.cod_emp = pedido.cod_emp;
                    pedidoDb.fec_ped = pedido.fec_ped;
                    pedidoDb.act_ped = pedido.act_ped;
                    pedidoDb.est_ped = pedido.est_ped;

                    //Crear HashSet con los códigos de productos
                    var codigosEnviados = new HashSet<int>(Productos);

                    //Verificar si los productos del pedido existen en la lista de productos
                    foreach (var productoExistente in pedidoDb.Pedido_Producto)
                    {
                        if (!codigosEnviados.Contains(productoExistente.cod_prod))
                        {
                            //Cambiar su estado a falso
                            productoExistente.est_pp = false;           
                        }
                    }

                    // Agregar nuevos productos
                    for (int i = 0; i < Productos.Length; i++)
                    {
                        //Buscar productos por código
                        int codProducto = Productos[i];
                        Producto producto = db.Producto.FirstOrDefault(p => p.cod_prod == codProducto);

                        if (producto != null)
                        {
                            
                            //Buscar productos relacionados al pedido
                            var productoExistente = pedidoDb.Pedido_Producto.FirstOrDefault(pp => pp.cod_prod == codProducto);


                            if (productoExistente != null)
                            {
                                //Si el producto estaba relacionado al pedido, activarlo
                                productoExistente.est_pp = true;
                            }
                            else {
                                //Agregar nuevas relaciones Pedido_Producto
                                pedidoDb.Pedido_Producto.Add(new Pedido_Producto
                                {
                                    cod_prod = producto.cod_prod,
                                    pre_prod = producto.prec_list_prod,
                                    can_prod = Cantidades[i],
                                    est_pp = true
                                });
                            }
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.cod_cli = new SelectList(db.Cliente, "cod_cli", "nom_cli", pedido.cod_cli);
            ViewBag.cod_emp = new SelectList(empleados, "cod_emp", "nom_completo", pedido.cod_emp);
            ViewBag.lstProductos = new SelectList(db.Producto, "cod_prod", "nom_prod");
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            pedido.est_ped = false;
            db.Entry(pedido).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Habilitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            switch (pedido.est_ped)
            {
                case true: pedido.est_ped = false; break;
                default: pedido.est_ped = true; break;
            }
            db.Entry(pedido).State = EntityState.Modified;
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
