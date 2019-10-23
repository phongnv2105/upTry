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
    public class SizesController : Controller
    {
        Repository<Size> _Size;
        Repository<ProductSize> _proSize;
        Repository<OrderDetail> _od;
        public SizesController()
        {
            _Size = new Repository<Size>();
            _proSize = new Repository<ProductSize>();
            _od = new Repository<OrderDetail>();
        }
        // GET: Admin/Sizes
        public ActionResult Index()
        {
            return View(_Size.GetAll());
        }

        // GET: Admin/Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            return View(_Size.Get(id));
        }

        // GET: Admin/Sizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sizes/Create
        [HttpPost]
        public ActionResult Create(Size s)
        {
            try
            {
                if (_Size.GetAll().Any(x => x.SizeName.ToLower().Equals(s.SizeName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                if (ModelState.IsValid)
                {
                    s.CreateAt = DateTime.Now;
                    if (_Size.Add(s))
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-success",
                            Message = "Thêm thành công !"
                        };
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "có lỗi trong quá trình chỉnh sửa");
                    return View();
                }

                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                return View(_Size.Get(id));
            }
            
        }

        // POST: Admin/Sizes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Size s )
        {
            if (ModelState.IsValid)
            {
                s.CreateAt = DateTime.Now;
                if (_Size.GetAll().Any(x => x.SizeName.ToLower().Equals(s.SizeName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                if (_Size.Edit(s))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Sửa thành công !"
                    };
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Chỉnh Sửa");
                return View();
            }
            
            return View();
        }

        // GET: Admin/Sizes/Delete/5
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
                    if (_proSize.Get(id) != null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Kích thước cấp này đang được sử dụng bởi sản phẩm"
                        };
                    }
                    if (_od.Get(id) != null)
                    {
                        TempData["msg"] = new ResponseMessage()
                        {
                            Type = "alert-danger",
                            Message = "Kích thước cấp này đang được đặt hàng"
                        };
                    }
                    _Size.Remove(id);
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
