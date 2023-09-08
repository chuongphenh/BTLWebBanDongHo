using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Drawing;
using WebBanDongHo.Models;

namespace WebBanDongHo.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {

            return View();
        }

        DaQldongHoContext _en = new DaQldongHoContext();

        [HttpGet]
        public JsonResult LoadData(Int64? idDongHo)
        {
            try
            {
                var lstDanhSachDongHo = _en.LstLoaiDongHos.ToList();

                var lstChiTietDongHoNam = _en.ChiTietDongHos.AsEnumerable().Where(c => c.DuongKinhMat >= 30).Select(c => new
                {
                    c.IdDongHo,
                    c.UrlAnh,
                    c.IdLoaiDongHo,
                    TenLoai = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
                    ? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().TenLoai : "",
                    GiaBan =  string.Format("{0:#,##0}", c.GiaBan) + "vnđ",
                    c.Sale,
                    GiaSale = c.Sale != null ? string.Format("{0:#,##0}", (c.GiaBan + ((c.Sale*c.GiaBan)/100))) : "",
                }).Take(8).ToList();

                return Json(new
                {
                    lstChiTietDongHoNam,
                    message = "Cập nhật thành công",
                    status = true
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = "Có lỗi khi lưu, chi tiết: " + ex.Message,
                    status = false,
                });
            }
        }
    }
}
