using obs.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace obs.Controllers
{
    public class SecurityController : Controller
    {
        private sporEntities db = new sporEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user user)
        {
            var userInDb = db.user.FirstOrDefault(x => x.useremail == user.useremail && x.userpasswd == user.userpasswd);

            if (userInDb != null)
            {
                FormsAuthentication.SetAuthCookie(user.useremail, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya parola.";
                return View();
            }
        }
    }
}
