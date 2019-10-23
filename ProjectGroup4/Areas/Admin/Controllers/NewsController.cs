﻿using Project.BAL.Repositories;
using Project.Models.DataMapper;
using ProjectGroup4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        Repository<News> _new;
        public NewsController()
        {
            _new = new Repository<News>();
        }
        // GET: Admin/News
        public ActionResult Index()
        {
            return View(_new.GetAll());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            return View(_new.Get(id));
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        [HttpPost]
        public ActionResult Create(News n)
        {
            if (ModelState.IsValid)
            {
                if (_new.Add(n))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Thêm thành công!"
                    };
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Có lỗi trong quá trình thêm mới");
                return View();
            }
            return View();
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                return View(_new.Get(id));
            }
           
        }

        // POST: Admin/News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, News n)
        {
            if (ModelState.IsValid)
            {
                if (_new.Edit(n))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Sửa thành công!"
                    };
                    return RedirectToAction("index");
                    
                }
                ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Chỉnh Sửa");
                return View();
            }
            return View();
        
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id ==0)
            {
                return View("_404");
            }
            else
            {
                try
                {
                    _new.Remove(id);
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Xóa thành công!"
                    };
                }
                catch (Exception)
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-danger",
                        Message = "Có Lỗi Trong Quá Trình Xóa"
                    };
                }
                
            }

            return View();
        }

        
    }
}
