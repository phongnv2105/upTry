using Project.BAL.Repositories;
using Project.Models.DataMapper;
using ProjectGroup4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class ColorsController : Controller
    {
        Repository<Color> _color;
        Repository<ProductColor> _proC;
        Repository<OrderDetail> _orD;
        public ColorsController()
        {
            _color = new Repository<Color>();
            _proC = new Repository<ProductColor>();
            _orD = new Repository<OrderDetail>();
        }
        // GET: Admin/Colors
        public ActionResult Index()
        {
            return View(_color.GetAll());
        }

        // GET: Admin/Colors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            return View(_color.Get(id));
        }

        // GET: Admin/Colors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Colors/Create
        [HttpPost]
        public ActionResult Create(Color c)
        {
            if (ModelState.IsValid)
            {
                c.CreateAt = DateTime.Now;
                if (_color.GetAll().Any(x => x.ColorName.ToLower().Equals(c.ColorName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                if (_color.Add(c))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Thêm Thành Công ID = " + c.ColorId,
                    };
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Thêm Mới");
                return View();
            }
            return View();
        }

        // GET: Admin/Colors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                return View(_color.Get(id));
            }
            
        }

        // POST: Admin/Colors/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Color c)
        {
            if (ModelState.IsValid)
            {
                c.CreateAt = DateTime.Now;
                //if (_color.GetAll().Any(x => x.ColorName.ToLower().Equals(c.ColorName.ToLower())))
                //{
                //    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                //    return View();
                //}
                if (_color.Edit(c))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Sửa Thành Công Thuộc Tính Có ID = " + c.ColorId,
                    };
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Sửa");
                return View();
            }
            return View();
        }

        // GET: Admin/Colors/Delete/5
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
                    if (_proC.Get(id) != null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Màu này đang được dùng với sản phẩm"
                        };
                    }
                    if (_orD.Get(id) ==  null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Màu này đã có trong đơn hàng của khách"
                        };
                    }
                    _color.Remove(id);
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
