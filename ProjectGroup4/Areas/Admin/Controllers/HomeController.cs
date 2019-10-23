using Project.BAL.Repositories;
using Project.Models.DataMapper;
using Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Repository<Users> _user;
        public HomeController()
        {
            _user = new Repository<Users>();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin u)
        {
            if (ModelState.IsValid)
            {
                var _u = _user.SingleBy(x => x.Email == u.UserName && x.Password == u.Password);
                if (_u != null) // Đăng nhập thành công!
                {
                    Session["user"] = _u;
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }
    }
}