using Project.BAL.Repositories;
using Project.Models.DataMapper;
using ProjectGroup4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class AttributesController : Controller
    {
        Repository<Attributes> _Att;
        Repository<AttributeType> _AttType;
        Repository<ProductAttribute> _ProAtt;
        public AttributesController()
        {
            _Att = new Repository<Attributes>();
            _AttType = new Repository<AttributeType>();
            _ProAtt = new Repository<ProductAttribute>();
        }
        // GET: Admin/Attributes
        public ActionResult Index()
        {
            ViewBag.AttType = _AttType.GetAll();
            
            return View(_Att.GetAll().AsQueryable().Include(x => x.AttributeType));
        }

        // GET: Admin/Attributes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            return View(_Att.Get(id));
        }

        // GET: Admin/Attributes/Create
        public ActionResult Create()
        {
            ViewBag.AtttypeId = new SelectList(_AttType.GetAll(), "AtttypeId", "TypeName");
            return View();
        }

        // POST: Admin/Attributes/Create
        [HttpPost]
        public ActionResult Create(Attributes a)
        {
            ViewBag.AtttypeId = new SelectList(_AttType.GetAll(), "AtttypeId", "TypeName");

            if (ModelState.IsValid)
            {
                if (_Att.GetAll().Any(x => x.AttributeName.ToLower().Equals(a.AttributeName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Thuộc tính tồn tại!");
                    return View();
                }
                if (_Att.Add(a))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Thêm Thành Công Thuộc Tính Có ID = " + a.AtttypeId,
                    };
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Có lỗi Trong Quá Tạo Mới");
                return View();
            }
            ModelState.AddModelError("", "Có lỗi Trong Quá Tạo Mới");
            return View();
        }

        // GET: Admin/Attributes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                return View(_Att.Get(id));
            }
            
        }

        // POST: Admin/Attributes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Attributes a)
        {
            if (ModelState.IsValid)
            {
                if (_Att.GetAll().Any(x => x.AttributeName.ToLower().Equals(a.AttributeName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Thuộc tính tồn tại!");
                    return View();
                }
                if (_Att.Edit(a))
                    {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Sửa Thành Công Thuộc Tính Có ID = " + a.AttributeId,
                    };
                    return RedirectToAction("Index");
                    }
                ModelState.AddModelError("", "Có lỗi Trong Quá Trình Sửa");
                return View();   
            }
            ModelState.AddModelError("", "Có lỗi Trong Quá Trình Sửa");
            return View();
            
        }

        // GET: Admin/Attributes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return View("_404");

            }
            else {
                try
                {
                    if (_ProAtt.Get(id) != null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Thuộc Tính Này Đang Được Dùng"
                        };
                    }
                    _Att.Remove(id);
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
