using Microsoft.AspNetCore.Mvc;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Authentication;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DuyetDonHangController : Controller
    {
        [Authentication]
        public IActionResult DuyetDonHang()
        {
            return View();
        }
        DaQldongHoContext _en = new DaQldongHoContext();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var lstTableDonGui = _en.DonDatHangs.Where(c => c.IsGui == true).Select(c => new
                {
                    c.IdDonDat,
                    c.HoTen,
                    c.DiaChi,
                    c.SoDienThoai,
                    c.Email,
                    NgayLap = c.NgayLap != null ? string.Format("{0:dd/MM/yyyy}", c.NgayLap) : "",
                }).ToList();

                return Json(new
                {
                    lstTableDonGui,
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

        [HttpGet]
        public JsonResult LoadTableDonHang(Int64? IdDonDat)
        {
            try
            {

                var data = _en.ChiTietDonDats.Where(c => c.IdDonDat == IdDonDat).ToList();

                var lstTableDonGui = data.AsEnumerable().Where(c => c.IdDonDat == IdDonDat).Select(c => new
                {
                    c.IdChiTietDonDat,
                    c.IdDongHo,
                    c.IdDonDat,
                    SoLuongMua = c.SoLuong,
                    UrlAnh = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().UrlAnh,
                    IdLoaiDongHo = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().IdLoaiDongHo,
                    GiaBan = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().GiaBan,
                    Sale = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().Sale,
                    SoLuongKho = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().SoLuong,
                }).Select(c => new
                {
                    c.IdChiTietDonDat,
                    c.IdLoaiDongHo,
                    c.IdDongHo,
                    c.UrlAnh,
                    TenLoai = _en.LstLoaiDongHos.AsEnumerable().Where(p => p.IdLoaiDongHo == c.IdLoaiDongHo).FirstOrDefault().TenLoai,
                    GiaBanT = string.Format("{0:#,##0}", c.GiaBan),
                    c.GiaBan,
                    c.SoLuongMua,
                    c.SoLuongKho,
                    GiaSale = c.Sale != null ? string.Format("{0:#,##0}", (c.GiaBan + ((c.Sale * c.GiaBan) / 100))) : "",
                }).ToList();

                return Json(new
                {
                    lstTableDonGui,
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
