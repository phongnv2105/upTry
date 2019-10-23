using Project.BAL.Repositories;
using Project.Models.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        Repository<Users> _User;
        public UsersController()
        {
            _User = new Repository<Users>();
        }
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(_User.GetAll());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
