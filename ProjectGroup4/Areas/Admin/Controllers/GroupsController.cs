using Project.BAL.Repositories;
using Project.Models.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectGroup4.Areas.Admin.Controllers
{
    public class GroupsController : Controller
    {
        Repository<Group> _group;
        Repository<Business> _business;
        Repository<Role> _role;
        Repository<GroupRoles> _groupRole;
        public GroupsController()
        {
            _group = new Repository<Group>();
            _business = new Repository<Business>();
            _role = new Repository<Role>();
            _groupRole = new Repository<GroupRoles>();
        }
        public ActionResult Index()
        {
            ViewBag.businesses = _business.GetAll();
            ViewBag.roles = _role.GetAll();
            return View(_group.GetAll());
        }
        [HttpPost]
        public ActionResult Grant(GroupRoles gr)
        {
            string msg = "";
            // 1. Gán
            // 2. Hủy
            // Kiểm tra quyền đã có hay chưa
            if (_groupRole.GetAll().Any(x => x.GroupId == gr.GroupId && x.BusinessId == gr.BusinessId && x.RoleId == gr.RoleId))
            {
                // Quyền đã tồn tại => Hủy
                var _gr = _groupRole.SingleBy(x => x.GroupId == gr.GroupId && x.BusinessId == gr.BusinessId && x.RoleId == gr.RoleId);
                // Hủy quyền (xóa)
                _groupRole.Remove(_gr);
                msg = "Hủy quyền thành công!";
            }
            else
            {
                // Quyền chưa tồn tại => Gán quyền
                _groupRole.Add(gr);
                msg = "Gán quyền thành công!";
            }
            return Json(new
            {
                StatusCode = 200,
                Message = msg
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRoles(int id)
        {
            var data = _groupRole.GetBy(x => x.GroupId == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
