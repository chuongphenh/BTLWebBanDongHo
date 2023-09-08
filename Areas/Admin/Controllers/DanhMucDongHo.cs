using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Authentication;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucDongHo : Controller
    {
        [Authentication]
        public IActionResult ViewDanhMucDongHo()
        {
            return View();
        }
        DaQldongHoContext _en = new DaQldongHoContext();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var lstThuongHieu = _en.DAnhMucHeThongs.Where(c => c.CbLoaiDanhMuc == "Danh mục thương hiệu").Select(c => new
                {
                    c.IdDanhMuc,
                    c.TenDanhMuc,
                }).ToList();

                var lstHangSx = _en.DAnhMucHeThongs.Where(c => c.CbLoaiDanhMuc == "Danh mục hãng sản xuất").Select(c => new
                {
                    c.IdDanhMuc,
                    c.TenDanhMuc,
                }).ToList();


                return Json(new
                {
                    lstThuongHieu,
                    lstHangSx,
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

        //Lưu thông tin
        [HttpPost]
        public IActionResult SaveData(string data)
        {

            try
            {
                var ClientData = JsonConvert.DeserializeObject<LstLoaiDongHo>(data);

                if (ClientData != null)
                {
                    //thêm mới
                    if (ClientData.IdLoaiDongHo == 0)
                    {
                        _en.LstLoaiDongHos.Add(ClientData);
                        _en.SaveChanges();
                    }
                    else
                    {
                        //cập nhập
                        var ServerData = _en.LstLoaiDongHos.Find(ClientData.IdLoaiDongHo);
                        if (ServerData != null)
                        {
                            ServerData.TenLoai = ClientData.TenLoai;
                            ServerData.HangSanXuat = ClientData.HangSanXuat;
                            ServerData.ThuongHieu = ClientData.ThuongHieu;
                            ServerData.IsTrangThai = ClientData.IsTrangThai;

                            _en.SaveChanges();
                        }
                    }
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

        // Xóa bản ghi được chọn
        [HttpPost]
        public IActionResult DeleteData(Int64? idLoaiDongHo)
        {
            try
            {
                var _data = _en.LstLoaiDongHos.Where(c => c.IdLoaiDongHo == idLoaiDongHo).First();
                if (_data != null)
                {
                    _en.LstLoaiDongHos.Remove(_data);
                    _en.SaveChanges();
                }

                return Json(new
                {
                    message = "Xóa thành công!",
                    status = true
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
        public JsonResult LoadTable(string tkThuongHieu, string tkHangSanXuat)
        {
            try
            {
                var data = _en.LstLoaiDongHos.AsEnumerable().Where(c =>( !String.IsNullOrEmpty(tkThuongHieu) ? tkThuongHieu == c.ThuongHieu : true)
                && ( !String.IsNullOrEmpty(tkHangSanXuat) ? tkHangSanXuat == c.HangSanXuat : true)
                ).Select(c => new
                {
                    c.IdLoaiDongHo,
                    c.TenLoai,
                    c.HangSanXuat,
                    c.ThuongHieu,
                    IsTrangThai = c.IsTrangThai == true ? "Còn Kinh Doanh" : "Ngưng sản xuất",
                }).ToList();

                var lstTable = data;

                return Json(new
                {
                    lstTable,
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
        public JsonResult LoadDetail(Int64? idLoaiDongHo)
        {
            try
            {
                var data = _en.LstLoaiDongHos.AsEnumerable().Where(c => idLoaiDongHo == c.IdLoaiDongHo).Select(c => new
                {
                    c.IdLoaiDongHo,
                    c.TenLoai,
                    c.ThuongHieu,
                    c.HangSanXuat,
                    c.IsTrangThai,
                }).FirstOrDefault();

                return Json(new
                {
                    data,
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
