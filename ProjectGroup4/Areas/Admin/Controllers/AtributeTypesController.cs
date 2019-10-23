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
    public class AtributeTypesController : Controller
    {
        Repository<AttributeType> _AttP;
        Repository<Attributes> _Att;
        public AtributeTypesController()
        {
            _AttP = new Repository<AttributeType>();
            _Att = new Repository<Attributes>();
        }
        // GET: Admin/AtributeTypes
        public ActionResult Index()
        {
            return View(_AttP.GetAll());
        }

        // GET: Admin/AtributeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            return View(_AttP.Get(id));
        }

        // GET: Admin/AtributeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AtributeTypes/Create
        [HttpPost]
        public ActionResult Create(AttributeType At)
        {
            if (ModelState.IsValid)
            {
                if (_AttP.GetAll().Any(x => x.TypeName.ToLower().Equals(At.TypeName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên kiểu thuộc tính đã tồn tại!");
                    return View();
                }
                if (_AttP.Add(At))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Thêm Thành Công Kiểu Thuộc Tính Có ID = "+At.AtttypeId,
                    };
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Có lỗi trong quá trình thêm mới");
                return View();
            }
            ModelState.AddModelError("", "Có lỗi trong quá trình thêm mới");
            return View();
        }

        // GET: Admin/AtributeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                return View(_AttP.Get(id));
            }
            
        }

        // POST: Admin/AtributeTypes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AttributeType At)
        {
            
                if (ModelState.IsValid)
                {
                    if (_AttP.GetAll().Any(x => x.TypeName.ToLower().Equals(At.TypeName.ToLower())))
                    {
                        ModelState.AddModelError("", "Tên kiểu thuộc tính đã tồn tại!");
                        return View();
                    }
                    if (_AttP.Edit(At))
                    {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Sửa Thành Công Kiểu Thuộc Tính ID là "+ id,
                    };
                    return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Có lỗi trong quá trình chỉnh sửa");
                    return View();
                }
                ModelState.AddModelError("", "Có lỗi trong quá trình chỉnh sửa");
                return View();
            
            
        }

        // GET: Admin/AtributeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
                else
            {
                    try
                    {
                    if (_Att.Get(id) != null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Kiểu Thuộc Tính Này Đang Được Dùng"
                        };
                    }
                        _AttP.Remove(id);
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

