using Project.BAL.Repositories;
using Project.Models.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Project.Models.ViewModels.CustomerViewModel;

namespace ProjectGroup4.Controllers
{
    public class AccountsController : Controller
    {
        Repository<Customer> customers;
        public AccountsController()
        {
            customers = new Repository<Customer>();
        }
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CustomerLogin c)
        {
            if (ModelState.IsValid)
            {
                var cus = customers.GetAll().SingleOrDefault(x => x.Email == c.Email && x.Password == c.Password);
                if (cus != null) // Tìm thấy
                {
                    Session["cus"] = cus;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                    return View();
                }
            }
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(CustomerRegister c)
        {
            if (ModelState.IsValid)
            {
                Customer cus = new Customer();
                cus.Email = c.Email;
                cus.Password = c.Password;
                cus.FullName = c.FirstName;
                cus.Phone = c.Phone;


                if (customers.Add(cus))
                {
                    return RedirectToAction("Index", "Home");
                };
                return View();
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("cus");
            return RedirectToAction("Index", "Home");
        }
    }
}