using Project.BAL.Repositories;
using Project.Models.DataMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Controllers
{
    public class ProductsController : Controller
    {
        Repository<Product> _product;
        Repository<Category> _cat;


        Repository<Attributes> _attr;
        Repository<AttributeType> _attrType;
        Repository<ProductAttribute> _proAttr;
        Repository<Size> _size;
        Repository<Color> _color;
        public ProductsController()
        {
            _product = new Repository<Product>();
            _cat = new Repository<Category>();
            _attr = new Repository<Attributes>();
            _proAttr = new Repository<ProductAttribute>();
            _attrType = new Repository<AttributeType>();
            _size = new Repository<Size>();
            _color = new Repository<Color>();
        }
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult ProductDetail(int id)
        {
            var _p = _product.GetAll().AsQueryable().Include(x => x.ProductAttributes).FirstOrDefault(x => x.ProductId == id);
            //var _p = _product.Get(id);
            var data = (
                               from p in _p.ProductAttributes
                               join attr in _attr.GetAll() on p.AttributeId equals attr.AttributeId
                               join t in _attrType.GetAll() on attr.AtttypeId equals t.AtttypeId
                               where t.Important == true
                               group attr by new { t.AtttypeId, t.TypeName } into gr
                               select new
                               {
                                   AtttypeId = gr.Key.AtttypeId,
                                   TypeName = gr.Key.TypeName,
                                   Attributes = gr.ToList()
                               }).Select(x => new AttributeType
                               {
                                   AtttypeId = x.AtttypeId,
                                   TypeName = x.TypeName,
                                   Attributes = x.Attributes
                               }).ToList();
            ViewBag.types = data;
            ViewBag.Size = _size.GetAll();
            ViewBag.Color = _color.GetAll();
            return View(_p);




        }


        public PartialViewResult ProductView()
        {
            DateTime GetDay = DateTime.Now;
            DateTime SDay = GetDay.AddDays(-7);
            var data = _product.GetBy(x => x.Status == true).Where(x => SDay < x.CreateDate);
            return PartialView("_ProductView", data);
        }

        public PartialViewResult ProductViewAll()
        {

            var data = _product.GetAll().Where(x => x.Status == true);
            return PartialView("_ProductViewAll", data);
        }
    }
}