using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebBanDongHo.Models;

namespace WebBanDongHo.Controllers
{
    public class SanPhamDetailController : Controller
    {
        public IActionResult SanPhamDetail()
        {
            return View();
        }

        DaQldongHoContext _en = new DaQldongHoContext();


        [HttpGet]
        public JsonResult LoadData(Int64? IdDongHo)
        {
            try
            {
                var lstDanhSachDongHo = _en.LstLoaiDongHos.ToList();
                var lstChiTietDongHoCon = _en.Documents.Where(c => c.IdDongHo == IdDongHo).Select(c => new { c.UrlAnh }).ToList();
                var lstChiTietDongHo = _en.ChiTietDongHos.AsEnumerable().Where(c => c.IdDongHo == IdDongHo).Select(c => new
                {
                    c.IdDongHo,
                    c.IdLoaiDongHo,
                    c.MaCode,
                    c.SoLuong,
                    c.KieuDang,
                    c.DungLuong,
                    c.ChiuNuoc,
                    c.CbChatLieuMat,
                    c.DuongKinhMat,
                    c.CbChatLieuDay,
                    c.KichThuocDay,
                    c.CbChatLieuVo,
                    c.KichThuocVo,
                    c.BaoHanh,
                    DtNgayTao = c.DtNgayTao != null ? string.Format("{0:yyyy-MM-dd}", c.DtNgayTao) : "",
                    UrlAnh = c.UrlAnh == "/ImageDongHo/" ? "" : c.UrlAnh,
                    TenLoai = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
                    ? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().TenLoai : "",
                    TenThuongHieu = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
                    ? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().ThuongHieu : "",
                    GiaBan = string.Format("{0:#,##0}", c.GiaBan) + "vnđ",
                    c.Sale,
                    GiaSale = c.Sale != null ? string.Format("{0:#,##0}", (c.GiaBan + ((c.Sale * c.GiaBan) / 100))) : "",
                }).FirstOrDefault();

                return Json(new
                {
                    lstChiTietDongHo,
                    lstChiTietDongHoCon,
                    messagse = "Cập nhật thành công",
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
        public JsonResult DatHang(Int64? IdDongHo)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    return Json(new
                    {
                        messagse = "Cập nhật thành công",
                        status = false
                    });
                }else
                {
                    Int64 IdDonDat = 0;
                    string UserName = HttpContext.Session.GetString("UserName");

                    var lst = _en.UserLogins.Where(c => c.UserName == UserName).FirstOrDefault();

                    var data =  _en.DonDatHangs.Where(c => c.UserId == lst.UserId).FirstOrDefault();
                    if (data == null)
                    {
                        DonDatHang donDatHang = new DonDatHang();
                        donDatHang.UserId = lst.UserId;
                        donDatHang.NgayLap = DateTime.Now;
                        donDatHang.DiaChi = lst.DiaChi;
                        donDatHang.Email = lst.Email;
                        donDatHang.HoTen = lst.HoTen;

                        _en.DonDatHangs.Add(donDatHang);
                        _en.SaveChanges();
                        IdDonDat = donDatHang.IdDonDat;
                    }
                    else
                    {
                        IdDonDat = data.IdDonDat;
                    }
                

                    ChiTietDonDat chiTietDonDat = new ChiTietDonDat();
                    chiTietDonDat.IdDonDat = IdDonDat;
                    chiTietDonDat.IdDongHo = IdDongHo;
                    chiTietDonDat.SoLuong = 1;

                    _en.ChiTietDonDats.Add(chiTietDonDat);
                    _en.SaveChanges();


                    return Json(new
                    {
                        messagse = "Cập nhật thành công",
                        status = true
                    });
                }    
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
