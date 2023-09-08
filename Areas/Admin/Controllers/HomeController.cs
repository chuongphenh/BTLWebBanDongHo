using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System.Security.Cryptography;
using System.Text;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Authentication;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        DaQldongHoContext _db = new DaQldongHoContext();

        [Authentication]
        public IActionResult DashBoard()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                string UserName = HttpContext.Session.GetString("UserName");

                var lst = _db.UserLogins.Where(c => c.UserName == UserName).FirstOrDefault();
                return View(lst);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult LoadData()
        {

            try
            {

                if (HttpContext.Session.GetString("UserName") != null)
                {
                    string UserName = HttpContext.Session.GetString("UserName");

                    var lstUserName = _db.UserLogins.Where(c => c.UserName == UserName).FirstOrDefault();

                    var lstThongBao = _db.ThongBaos.Where(c => c.UserIdNhan == lstUserName.UserId).ToList();

                    Int64 ThuGui = 0;
                    ThuGui = lstThongBao.Count();

                    return Json(new
                    {
                        ThuGui,
                        lstThongBao,
                        lstUserName,
                        status = true
                    });
                }
                return Json(new
                {
                    status = false
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = "Có lỗi khi xóa, chi tiết " + ex.Message,
                    status = false
                });
            }

        }

        [HttpGet]
        public IActionResult LogoutClient()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return Json(new
            {
                status = true
            });
        }
    }
}
