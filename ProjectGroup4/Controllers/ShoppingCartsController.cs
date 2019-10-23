using Project.BAL.Repositories;
using Project.Models.DataMapper;
using ProjectGroup4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Controllers
{
    public class ShoppingCartsController : Controller
    {
        Repository<Product> _pro;
        Repository<Color> _color;
        Repository<Size> _s;
        Repository<Orders> _orders;
        Repository<Attributes> _attributes;
        public ShoppingCartsController()
        {
            _pro = new Repository<Product>();
            _color = new Repository<Color>();
            _s = new Repository<Size>();
            _attributes = new Repository<Attributes>();
            _orders = new Repository<Orders>();
        }
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            List<ItemCart> lst = new List<ItemCart>();
            if (Session["cart"] != null)
            {
                // Lấy các sp trong session đưa ta list để thao tác
                lst = Session["cart"] as List<ItemCart>;
            }
            ViewBag.Attrs = _attributes.GetAll().AsQueryable().Include(x => x.AttributeType);

            return View(lst);
        }

        [HttpPost]
        public ActionResult AddToCart(ItemCart itemCart)
        {
            // Lấy sản phẩm
            var productId = Convert.ToInt32(Request["ProductId"]);

            var _product = _pro.Get(productId);
            itemCart.Product = _product;

            // Lấy attributes
            //itemCart.Attributes = Request["Attributes"];

            //// lấy color
            //itemCart.Color = Request["Color"];
            //// lấy Size

            //itemCart.Size = Request["Size"];

            // Thêm vào giỏ hàng
            List<ItemCart> lst = new List<ItemCart>();

            // Kiểm tra giỏ hàng có hay chưa
            if (Session["cart"] != null)// Đã có => Kiểm tra đã trùng sp trong giỏ chưa
            {
                // Lấy các sp trong session đưa ta list để thao tác
                lst = Session["cart"] as List<ItemCart>;
                var check = false;
                foreach (var item in lst)
                {
                    // Trùng =>  Cập nhật số lượng
                    if (item.Product.ProductId == productId && item.Attributes == itemCart.Attributes && item.Color == itemCart.Color && item.Size == itemCart.Size)
                    {
                        item.Quantity += itemCart.Quantity;
                        check = true;
                    }
                }
                // Chưa trùng thì thêm mới
                if (!check)
                {
                    lst.Add(itemCart);
                }
            }

            else // Chưa có => thêm mới sp và lưu vào giỏ
            {
                lst.Add(itemCart);
            }
            // Lưu lại giỏ hàng vào session
            Session["cart"] = lst;

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult UpdateCart(ItemCart itemCart)
        {
            var productId = Convert.ToInt32(Request["ProductId"]);

            var _product = _pro.Get(productId);
            itemCart.Product = _product;
            List<ItemCart> lst = new List<ItemCart>();
            // Đã có => Kiểm tra đã trùng sp trong giỏ chưa
            if (Session["cart"] != null)
            {
                // Lấy các sp trong session đưa ta list để thao tác
                lst = Session["cart"] as List<ItemCart>;
                var check = false;
                foreach (var item in lst)
                {
                    // Trùng =>  Cập nhật số lượng
                    if (item.Product.ProductId == productId && item.Attributes == itemCart.Attributes && item.Color == itemCart.Color && item.Size == itemCart.Size)
                    {
                        item.Quantity += itemCart.Quantity;
                        check = true;
                    }
                }
                // Chưa trùng thì thêm mới
                
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(ItemCart itemCart)
        {
            var productId = Convert.ToInt32(Request["ProductId"]);

            var _product = _pro.Get(productId);
            itemCart.Product = _product;
            List<ItemCart> lst = new List<ItemCart>();
            // Đã có => Kiểm tra đã trùng sp trong giỏ chưa
            if (Session["cart"] != null)
            {
                // Lấy các sp trong session đưa ta list để thao tác
                lst = Session["cart"] as List<ItemCart>;
                var check = false;
                foreach (var item in lst)
                {
                    // Trùng =>  Cập nhật số lượng
                    if (item.Product.ProductId == productId && item.Attributes == itemCart.Attributes && item.Color == itemCart.Color && item.Size == itemCart.Size)
                    {
                        item.Quantity += itemCart.Quantity;
                        check = true;
                    }
                }
                // Chưa trùng thì thêm mới

            }
            return RedirectToAction("Index");
        }


        public ActionResult Payment()
        {
            // Kiểm tra đăng nhập
            if (Session["cus"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            // Kiểm tra có giỏ hàng hay không
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index");
            }
            // Kiểm tra giỏ hàng có sp hay không
            List<ItemCart> lst = new List<ItemCart>();
            // Lấy các sp trong session đưa ta list để thao tác
            lst = Session["cart"] as List<ItemCart>;
            if (lst.Count == 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Payment(Orders order)
        {
            #region TẠO THÔNG TIN CHO ĐỐI TƯỢNG ORDER
            order.CreateDate = DateTime.Now;

            // Đưa OrderDetails vào Order
            // Kiểm tra giỏ hàng có sp hay không
            List<ItemCart> lst = new List<ItemCart>();
            // Lấy các sp trong session đưa ta list để thao tác
            lst = Session["cart"] as List<ItemCart>;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in lst)
            {
                OrderDetail od = new OrderDetail
                {
                    ProductId = item.Product.ProductId,
                    
                    Attributes = item.Attributes,
                    Quantity = item.Quantity,
                    ColorId = item.Color,
                    SizeId = item.Size,
                    Price = item.Product.PriceOut,
                   
                };
                orderDetails.Add(od);
            }
            order.OrderDetails = orderDetails;
            #endregion TẠO THÔNG TIN CHO ĐỐI TƯỢNG ORDER

            

            #region THÊM GIỎ HÀNG
            // Inser giỏ hàng
            if (!_orders.Add(order))
            {
                return View();
            }
            #endregion

            // Gửi Email
            // Tạo nội dung email
            string _header = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/header_order.txt"));
            string _footer = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/footer_order.txt"));
            #region Thông tin người nhận
            string _main = String.Format(@"<h2 class='title'>ĐƠN HÀNG BKAP{5}</h2>
				<p>
					<b>Họ tên người nhận</b>
					<span>{0}</span>
				</p>
				<p>
					<b>Email</b>
					<span>{1}</span>
				</p>
				<p>
					<b>SĐT</b>
					<span>{2}</span>
				</p>
				<p>
					<b>Địa chỉ</b>
					<span>{3}</span>
				</p>
				<p>
					<b>Ngày mua</b>
					<span>{4:dd/MM/yyyy | HH:mm:ss}</span>
				</p>", order.FullName, order.Email, order.Phone, order.Address, order.CreateDate, order.OrderId);
            #endregion
            #region Sản phẩm mua
            double gg = 0;
            double total = 0;
            _main += @"<table class='table text-center'>
					<thead>
						<tr>
							<th>Sản phẩm</th>
							<th>Đơn giá</th>
							<th>Số lượng</th>
							<th>Giảm giá</th>
							<th>Thực trả</th>
							<th>Thành tiền</th>
						</tr>
					</thead>
					<tbody>";
            foreach (var item in lst)
            {
                gg = (item.Product.PriceOut) - ((item.Product.Discount * (item.Product.PriceOut)) / 100);

                total += (item.Quantity * gg);
                _main += @"<tr>";
                _main += String.Format(@"<td>{0}</td>", item.Product.ProductName);
                _main += String.Format(@"<td>{0:#,##} VNĐ</td>", item.Product.PriceOut);
                _main += String.Format(@"<td>{0}</td>", item.Quantity);
                _main += String.Format(@"<td>{0}</td>", item.Product.Discount);
                _main += String.Format(@"<td>{0}</td>", gg);
                _main += String.Format(@"<td>{0:#,##} VNĐ</td>", total);
                _main += @"</tr>";
               
            }
            _main += String.Format(@"<tr>
							<th colspan='3' style='text-align: left'>Tổng tiền | Chiết khấu {0} %</th>
							<th>{1}</th>
						</tr>
					</tbody>
				</table>");
            #endregion
            Helpers.SendEmail(order.Email, "quantrivienwebsite.bkap@gmail.com", "quantriwebbkap", "[BKAPSHOP]_THÔNG TIN ĐƠN HÀNG", _header + _main + _footer);

            return RedirectToAction("Index", "Home");
        }


    }
}