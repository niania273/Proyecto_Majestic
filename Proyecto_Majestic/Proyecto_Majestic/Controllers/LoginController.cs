using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto_Majestic.Controllers
{
    public class LoginController : Controller
    {
        private BDMAJESTICEntities db = new BDMAJESTICEntities();
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {

            Usuario ousuario = db.Usuario.Where(usu => usu.ema_usu == usuario.ema_usu && usu.con_usu == usuario.con_usu).FirstOrDefault();

            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            FormsAuthentication.SetAuthCookie(usuario.ema_usu, false);

            Session["Rol"] = ousuario.Rol.nom_rol;
            Session["Codigo"] = ousuario.cod_usu;
            Session["Usuario"] = ousuario.nom_usu;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}