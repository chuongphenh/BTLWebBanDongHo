using Microsoft.AspNetCore.Mvc;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Authentication;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DatHangController : Controller
    {
        [Authentication]
        public IActionResult DatHang()
        {
            return View();
        }


        DaQldongHoContext _en = new DaQldongHoContext();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                string UserName = HttpContext.Session.GetString("UserName");

                var lst = _en.UserLogins.Where(c => c.UserName == UserName).FirstOrDefault();

                var lstDonHang2 = _en.DonDatHangs.AsEnumerable().Where(c => c.UserId == lst.UserId && c.IsGui != true).FirstOrDefault();

                if (lstDonHang2 != null)
                {

                    Int64 IdDonDat = lstDonHang2.IdDonDat;
                    var data = _en.ChiTietDonDats.ToList();

                    var lstDonHang = data.AsEnumerable().Where(c => c.IdDonDat == IdDonDat).Select(c => new
                    {
                        c.IdChiTietDonDat,
                        c.IdDongHo,
                        c.IdDonDat,
                        UrlAnh = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().UrlAnh,
                        IdLoaiDongHo = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().IdLoaiDongHo,
                        GiaBan = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().GiaBan,
                        Sale = _en.ChiTietDongHos.AsEnumerable().Where(p => p.IdDongHo == c.IdDongHo).FirstOrDefault().Sale,
                    }).Select(c => new
                    {
                        c.IdChiTietDonDat,
                        c.IdLoaiDongHo,
                        c.IdDongHo,
                        c.UrlAnh,
                        TenLoai = _en.LstLoaiDongHos.AsEnumerable().Where(p => p.IdLoaiDongHo == c.IdLoaiDongHo).FirstOrDefault().TenLoai,
                        GiaBanT = string.Format("{0:#,##0}", c.GiaBan),
                        c.GiaBan,
                        GiaSale = c.Sale != null ? string.Format("{0:#,##0}", (c.GiaBan + ((c.Sale * c.GiaBan) / 100))) : "",
                    }).ToList();

                    var TongGiaBan = lstDonHang.Sum(c => c.GiaBan);
                    var Tong = string.Format("{0:#,##0}", TongGiaBan);

                    return Json(new
                    {
                        lstDonHang,
                        Tong,
                        message = "Cập nhật thành công",
                        status = true
                    });
                }
                return Json(new
                {
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

        [HttpPost]
        public JsonResult DeleateData(Int64 IdChiTietDonDat)
        {
            try
            {
                var data = _en.ChiTietDonDats.Find(IdChiTietDonDat);

                if (data != null)
                {
                    _en.ChiTietDonDats.Remove(data);
                    _en.SaveChanges();
                }


                return Json(new
                {
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

        [HttpPost]
        public JsonResult GuiDuyet()
        {
            try
            {

                string UserName = HttpContext.Session.GetString("UserName");

                var lst = _en.UserLogins.Where(c => c.UserName == UserName).FirstOrDefault();

                var IsAdmin = _en.UserLogins.Where(c => c.IsAdmin == true).FirstOrDefault();

                var lstDonHang2 = _en.DonDatHangs.AsEnumerable().Where(c => c.UserId == lst.UserId && c.IsGui != true).FirstOrDefault();

                lstDonHang2.IsGui = true;


                ThongBao thongBao = new ThongBao();
                thongBao.UserIdGui = lst.UserId;
                thongBao.UserIdNhan = IsAdmin.UserId;
                thongBao.NoiDungGui = lst.HoTen + " gửi duyệt mua hàng ";
                thongBao.DuongDan = "Admin/DuyetDonHang/DuyetDonHang";

                _en.ThongBaos.Add(thongBao);
                _en.SaveChanges();

                return Json(new
                {
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
