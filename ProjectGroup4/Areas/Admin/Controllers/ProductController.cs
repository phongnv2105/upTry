using Project.BAL.Repositories; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models.DataMapper;
using System.Data.Entity;
using System.Text.RegularExpressions;
using ProjectGroup4.Models;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        Repository<Product> _Product;
        Repository<Provider> _Provider;
        Repository<CategoryProduct> _CategoryProduct;
        Repository<Category> _Category;
        Repository<Color> _Color;
        Repository<Size> _Size;
        Repository<Attributes> _Attribute;
        Repository<AttributeType> _Attr;
        Repository<ProductAttribute> _proAtt;
        Repository<OrderDetail> _od;
        Repository<ProductSize> _proSize;
        Repository<ProductColor> _proColor;
        // GET: Admin/Product
        public ProductController()
        {
            _Product = new Repository<Product>();
            _Provider = new Repository<Provider>();
            _CategoryProduct = new Repository<CategoryProduct>();
            _Category = new Repository<Category>();
            _Color = new Repository<Color>();
            _Size = new Repository<Size>();
            _Attribute = new Repository<Attributes>();
            _Attr = new Repository<AttributeType>();
            _proAtt = new Repository<ProductAttribute>();
            _od = new Repository<OrderDetail>();
            _proSize = new Repository<ProductSize>();
            _proColor = new Repository<ProductColor>();
        }
        public ActionResult Index()
        {

            ViewBag.Category = _Category.GetAll();
            return View(_Product.GetAll());
        }
        public ActionResult Details(int? id)
        {
            if (id == null || id== 0)
            {
                return View("_404");
            }
            return View(_Product.Get(id));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProviderId = new SelectList(_Provider.GetAll(), "ProviderId", "ProviderName");
            ViewBag.CatPro = _Category.GetAll();
            ViewBag.Color = _Color.GetAll();
            ViewBag.Size = _Size.GetAll();
            ViewBag.Attributse = _Attribute.GetAll();
            ViewBag.AttrTypes = _Attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            p.CreateDate = DateTime.Now;
            string url = Slug.GenerateSlug(p.ProductName);
            p.MetaLink = url;
            p.CoutView = 0;
            ViewBag.ProviderId = new SelectList(_Provider.GetAll(), "ProviderId", "ProviderName");
            ViewBag.CatPro = _Category.GetAll();
            ViewBag.Color = _Color.GetAll();
            ViewBag.Size = _Size.GetAll();
            ViewBag.Attribute = _Attribute.GetAll();
            ViewBag.AttrTypes = _Attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            if (p.CategoryProducts != null)
            {
                p.CategoryProducts.ToList().ForEach(x => x.ProductId = p.ProductId);
            }
            if(p.ProductSizes != null)
            {
                p.ProductSizes.ToList().ForEach(x => x.ProductId = p.ProductId);
            }
            if (p.ProductColors != null)
            {
                p.ProductColors.ToList().ForEach(x => x.ProductId = p.ProductId);
            }
            
            if (p.ProductAttributes != null)
            {
                p.ProductAttributes.ToList().ForEach(x => x.ProductId = p.ProductId);
            }
            if (ModelState.IsValid)
            {
                if (_Product.GetAll().Any(x => x.ProductName.ToLower().Equals(p.ProductName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                if (_Product.Add(p))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-success",
                        Message = "Thêm Thành Công Sản Phẩm Có ID = " + p.ProductName,
                    };
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Có Lỗi Trong Quá Trình Thêm Mới");
                return View();
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("_404");
            }
            else
            {
                ViewBag.ProviderId = new SelectList(_Provider.GetAll(), "ProviderId", "ProviderName");
                ViewBag.CatPro = _Category.GetAll();
                ViewBag.Color = _Color.GetAll();
                ViewBag.Size = _Size.GetAll();
                ViewBag.Attribute = _Attribute.GetAll();
                ViewBag.AttrTypes = _Attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();

                return View(_Product.Get(id));
            }
            
        }
        [HttpPost]
        public ActionResult Edit(int id, Product p)
        {
            ViewBag.ProviderId = new SelectList(_Provider.GetAll(), "ProviderId", "ProviderName");
            ViewBag.CatPro = _Category.GetAll();
            ViewBag.Color = _Color.GetAll();
            ViewBag.Size = _Size.GetAll();
            ViewBag.Attribute = _Attribute.GetAll();
            ViewBag.AttrTypes = _Attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            if (p.ProductColors != null)
            {
                _proColor.RemoveRange(_proColor.GetBy(x => x.ProductId == p.ProductId));
                p.ProductColors.ToList().ForEach(x => x.ProductId = p.ProductId);
                _proColor.AddRange(p.ProductColors);
            }
            if (ModelState.IsValid)
            {
                if (_Product.GetAll().Any(x => x.ProductName.ToLower().Equals(p.ProductName.ToLower())))
                {
                    ModelState.AddModelError("", "Tên Này Đã Tồn Tại !");
                    return View();
                }
                if (_Product.Edit(p))
                {
                    return RedirectToAction("Index");
                }
            }
            
            return View();
        }
        public ActionResult Delete(int id)
        {

            try
            {
                
                // Phải check trong bảng OrderDetail xem sp đã được mua hay chưa
                if (_od.Get(id) != null)
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-danger",
                        Message = "Sản Phẩm Này Đã Được Đặt Hàng"
                    };
                }
                // Xóa danh mục của sản phẩm
                _CategoryProduct.RemoveRange(_CategoryProduct.GetBy(x => x.ProductId == id));
                // xóa thuộc tính của sản phẩm
                _proAtt.RemoveRange(_proAtt.GetBy(x => x.ProductId == id));
                // xóa size của sản phẩm
                _proSize.RemoveRange(_proSize.GetBy(x => x.ProductId == id));
                // xóa color của sản phẩm
                _proColor.RemoveRange(_proColor.GetBy(x => x.ProductId == id));
                //Xóa sp
                _Product.Remove(id);
                TempData["msg"] = new ResponseMessage()
                {
                    Type = "alert-success",
                    Message = "Xóa thành công!"
                };
            }
            catch (Exception ex)
            {
                TempData["msg"] = new ResponseMessage()
                {
                    Type = "alert-danger",
                    Message = ex.Message
                };
            }
            return RedirectToAction("Index");
        }


    }
}