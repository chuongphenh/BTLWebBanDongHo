using Microsoft.AspNetCore.Mvc;
using WebBanDongHo.Models;

namespace WebBanDongHo.Controllers
{
	public class SanPhamController : Controller
	{
		public IActionResult SanPham()
		{
			return View();
		}


		DaQldongHoContext _en = new DaQldongHoContext();


		[HttpGet]
		public JsonResult LoadThuongHieu()
		{
			try
			{
				var lstDanhMucDongHo = _en.LstLoaiDongHos.ToList();

				var lstThuongHieu = _en.DAnhMucHeThongs.Where(c => c.CbLoaiDanhMuc == "Danh mục thương hiệu").AsEnumerable().Select(c => new
				{

					c.TenDanhMuc,
					SoLuong = lstDanhMucDongHo.AsEnumerable().Where(p => p.ThuongHieu == c.TenDanhMuc).Count(),
				}).ToList();


				return Json(new
				{
					lstThuongHieu,
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
		public JsonResult LoadChiTietSanPham(string TenThuongHieu,int page, int pageSize = 9)
		{
			try
			{
				var lstDanhSachDongHo = _en.LstLoaiDongHos.ToList();

				var lstDanhSachDongHo2 = _en.ChiTietDongHos.AsEnumerable().Select(c => new
				{
					TenThuongHieu = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
					? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().ThuongHieu : "",
				}).Where(p => p.TenThuongHieu == TenThuongHieu).ToList();

				var lstChiTietDongHoNam = _en.ChiTietDongHos.AsEnumerable().Select(c => new
				{
					c.IdDongHo,
					c.UrlAnh,
					c.IdLoaiDongHo,
					TenLoai = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
					? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().TenLoai : "",
					TenThuongHieu = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
					? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().ThuongHieu : "",
					GiaBan = string.Format("{0:#,##0}", c.GiaBan) + "vnđ",
					c.Sale,
					GiaSale = c.Sale != null ? string.Format("{0:#,##0}", (c.GiaBan + ((c.Sale * c.GiaBan) / 100))) : "",
				}).Where(p => p.TenThuongHieu == TenThuongHieu).Skip((page - 1) * pageSize).Take(pageSize).ToList();


				int total = lstDanhSachDongHo2.Count();

				return Json(new
				{
					lstChiTietDongHoNam,
					total,
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
		public JsonResult LoadData(int page, int pageSize = 9)
		{
			try
			{
				var lstDanhSachDongHo = _en.LstLoaiDongHos.ToList();

				var lstDanhSachDongHo2 = _en.ChiTietDongHos.ToList();

				var lstChiTietDongHoNam = _en.ChiTietDongHos.AsEnumerable().Select(c => new
				{
					c.IdDongHo,
					c.UrlAnh,
					c.IdLoaiDongHo,
					TenLoai = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
					? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().TenLoai : "",
					GiaBan = string.Format("{0:#,##0}", c.GiaBan) + "vnđ",
					c.Sale,
					GiaSale = c.Sale != null ? string.Format("{0:#,##0}", (c.GiaBan - ((c.Sale * c.GiaBan) / 100))) : "",
				}).Skip((page - 1) * pageSize).Take(pageSize).ToList();

				int total = lstDanhSachDongHo2.Count();

				return Json(new
				{
					lstChiTietDongHoNam,
					total,
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
