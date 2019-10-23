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
    public class CategoriesController : Controller
    {
        Repository<Category> _Category;
        Repository<CategoryProduct> _CatPro;
        public CategoriesController()
        {
            _Category = new Repository<Category>();
            _CatPro = new Repository<CategoryProduct>();
        }
        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(_Category.GetAll());
        }
        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            return View(_Category.Get(id));
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        public ActionResult Create(Category c)
        {
            c.MetaLink = Slug.GenerateSlug(c.CategoryName);
            if (ModelState.IsValid)
            {
                if (_Category.GetAll().Any(x => x.CategoryName.ToLower().Equals(c.CategoryName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                if (_Category.Add(c))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Thêm Thành Công ID = " + c.CategoryId,
                    };
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Thêm Mới");
                return View();
            }
            return View();
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                return View(_Category.Get(id));
            }
            
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category c)
        {
            if (ModelState.IsValid)
            {
                if (_Category.GetAll().Any(x => x.CategoryName.ToLower().Equals(c.CategoryName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                try
                {
                    if (_Category.Edit(c))
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-success",
                            Message = "Sửa Thành Công ID = " + c.CategoryId,
                        };
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Sửa Dữ Liệu");
                    return View();
                }
                catch
                {
                    ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Sửa Dữ Liệu");
                    return View();
                }
            }
            return View();
            
        }

        // GET: Admin/Categories/Delete/5
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
                    if (_CatPro.Get(id) != null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Danh Mục Này Hiện Tại Đang Được Dùng"
                        };
                    }
                    _Category.Remove(id);
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
                return RedirectToAction("Index");
            }
            
           
        }

        
    }
}
