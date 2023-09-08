using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Authentication;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucHeThong : Controller
    {
        [Authentication]
        public IActionResult ViewDanhMucHeThong()
        {
            return View();
        }

        DaQldongHoContext _en = new DaQldongHoContext();
        //Lưu thông tin
        [HttpPost]
        public IActionResult SaveData(string data)
        {
            try
            {
                var ClientData = JsonConvert.DeserializeObject<DAnhMucHeThong>(data);

                if (ClientData != null)
                {
                    //thêm mới
                    if (ClientData.IdDanhMuc == 0)
                    {
                        var countMa = _en.DAnhMucHeThongs.Where(c => c.MaDanhMuc == ClientData.MaDanhMuc && c.CbLoaiDanhMuc == ClientData.CbLoaiDanhMuc).Count();

                        if (countMa > 0)
                        {
                            return Json(new
                            {
                                status = false
                            });
                        }

                        _en.DAnhMucHeThongs.Add(ClientData);
                        _en.SaveChanges();
                    }
                    else
                    {
                        //cập nhập
                        var ServerData = _en.DAnhMucHeThongs.Find(ClientData.IdDanhMuc);
                        if (ServerData != null)
                        {
                            ServerData.MaDanhMuc = ClientData.MaDanhMuc;
                            ServerData.TenDanhMuc = ClientData.TenDanhMuc;
                            ServerData.CbLoaiDanhMuc = ClientData.CbLoaiDanhMuc;

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
        public IActionResult DeleteData(int? idDanhMuc)
        {
            try
            {
                var _data = _en.DAnhMucHeThongs.Where(c => c.IdDanhMuc == idDanhMuc).First();
                if (_data != null)
                {
                    _en.DAnhMucHeThongs.Remove(_data);
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
        public JsonResult LoadTable(string cbLoaiDanhMuc)
        {
            try
            {
                var data = _en.DAnhMucHeThongs.AsEnumerable().Where(c => cbLoaiDanhMuc == c.CbLoaiDanhMuc).Select(c => new
                {
                    c.IdDanhMuc,
                    c.MaDanhMuc,
                    c.TenDanhMuc,
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
        public JsonResult LoadDetail(int? IdDanhMuc)
        {
            try
            {
                var data = _en.DAnhMucHeThongs.AsEnumerable().Where(c => IdDanhMuc == c.IdDanhMuc).Select(c => new
                {
                    c.IdDanhMuc,
                    c.MaDanhMuc,
                    c.TenDanhMuc,
                    c.CbLoaiDanhMuc,
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
