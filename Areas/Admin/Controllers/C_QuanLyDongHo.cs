using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Authentication;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class C_QuanLyDongHo : Controller
    {

        [Authentication]
        public IActionResult QuanLyDongHo()
        {
            return View();
        }
        private IWebHostEnvironment webHostEnvironment;

        DaQldongHoContext _en = new DaQldongHoContext();
        public C_QuanLyDongHo(IWebHostEnvironment environment)
        {
            webHostEnvironment = environment;
        }

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var lstTenDongHo = _en.LstLoaiDongHos.Select(c => new
                {
                    c.IdLoaiDongHo,
                    c.TenLoai,
                }).ToList();


                var lstChatLieuMat = _en.DAnhMucHeThongs.Where(c => c.CbLoaiDanhMuc == "Danh mục chất liệu mặt").Select(c => new
                {
                    c.IdDanhMuc,
                    c.TenDanhMuc,
                }).ToList();

                var lstChatLieuDay = _en.DAnhMucHeThongs.Where(c => c.CbLoaiDanhMuc == "Danh mục chất liệu dây").Select(c => new
                {
                    c.IdDanhMuc,
                    c.TenDanhMuc,
                }).ToList();

                var lstChatLieuVo = _en.DAnhMucHeThongs.Where(c => c.CbLoaiDanhMuc == "Danh mục chất liệu vỏ").Select(c => new
                {
                    c.IdDanhMuc,
                    c.TenDanhMuc,
                }).ToList();


                return Json(new
                {
                    lstTenDongHo,
                    lstChatLieuMat,
                    lstChatLieuDay,
                    lstChatLieuVo,
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
        public async Task<IActionResult> SaveData(string str_JSON, string is_thay_doi)
        {
            try
            {

                string fileName = "";
                string imgPath = "";
                string Result = string.Empty;
                var Files = Request.Form.Files;
                foreach (IFormFile Source in Files)
                {
                    fileName = Source.Name;
                    imgPath = GetActualpath(fileName);

                    if (System.IO.File.Exists(imgPath))
                        System.IO.File.Delete(imgPath);

                    using (FileStream stream = System.IO.File.Create(imgPath))
                    {
                        await Source.CopyToAsync(stream);
                    }
                }

                var ClientData = JsonConvert.DeserializeObject<ChiTietDongHo>(str_JSON);
                if (ClientData.IdDongHo == 0)
                {
                    ClientData.UrlAnh = "/ImageDongHo/" + fileName;
                    _en.ChiTietDongHos.Add(ClientData);
                    _en.SaveChanges();
                }
                else
                {
                    //cập nhập
                    var ServerData = _en.ChiTietDongHos.Find(ClientData.IdDongHo);
                    if (ServerData != null)
                    {
                        ServerData.IdLoaiDongHo = ClientData.IdLoaiDongHo;
                        ServerData.MaCode = ClientData.MaCode;
                        ServerData.GiaBan = ClientData.GiaBan;
                        ServerData.Sale = ClientData.Sale;
                        ServerData.SoLuong = ClientData.SoLuong;
                        ServerData.KieuDang = ClientData.KieuDang;
                        ServerData.DungLuong = ClientData.DungLuong;
                        ServerData.ChiuNuoc = ClientData.ChiuNuoc;
                        ServerData.CbChatLieuMat = ClientData.CbChatLieuMat;
                        ServerData.DuongKinhMat = ClientData.DuongKinhMat;
                        ServerData.CbChatLieuDay = ClientData.CbChatLieuDay;
                        ServerData.KichThuocDay = ClientData.KichThuocDay;
                        ServerData.CbChatLieuVo = ClientData.CbChatLieuVo;
                        ServerData.KichThuocVo = ClientData.KichThuocVo;
                        ServerData.BaoHanh = ClientData.BaoHanh;
                        ServerData.DtNgayTao = ClientData.DtNgayTao;

                        if (fileName != "")
                        {
                            ServerData.UrlAnh = "/ImageDongHo/" + fileName;
                        }
                        _en.SaveChanges();
                    }


                }

                return Json(new
                {
                    message = "Cập nhật thành công",
                    status = true
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    message = "Có lỗi khi lưu, chi tiết: ",
                    status = false,
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SaveFileAnhCon(Int64? IdDongHo)
        {
            try
            {

                string fileName = "";
                string imgPath = "";
                string Result = string.Empty;
                var Files = Request.Form.Files;
                foreach (IFormFile Source in Files)
                {
                    fileName = Source.Name;
                    imgPath = GetActualpath(fileName);

                    if (System.IO.File.Exists(imgPath))
                        System.IO.File.Delete(imgPath);

                    using (FileStream stream = System.IO.File.Create(imgPath))
                    {
                        await Source.CopyToAsync(stream);
                    }
                }

                Document document = new Document();
                document.TenFile = fileName;
                document.UrlAnh = "/ImageDongHo/" + fileName;
                document.IdDongHo = IdDongHo;


                _en.Documents.Add(document);
                _en.SaveChanges();


                return Json(new
                {
                    message = "Cập nhật thành công",
                    status = true
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    message = "Có lỗi khi lưu, chi tiết: ",
                    status = false,
                });
            }

        }
        [HttpGet]
        public JsonResult LoadTable()
        {
            try
            {

                var lstDanhSachDongHo = _en.LstLoaiDongHos.ToList();

                var lstTableDongHo = _en.ChiTietDongHos.AsEnumerable().Select(c => new
                {
                    c.IdDongHo,
                    c.MaCode,
                    c.SoLuong,
                    c.KieuDang,
                    c.DungLuong,
                    TenLoai = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
                    ? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().TenLoai : "",
                    HangSanXuat = lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault() != null
                    ? lstDanhSachDongHo.Where(t => t.IdLoaiDongHo == c.IdLoaiDongHo).AsEnumerable().FirstOrDefault().HangSanXuat : "",
                    UrlAnh = c.UrlAnh == "/ImageDongHo/" ? "" : c.UrlAnh,

                }).ToList();

                return Json(new
                {
                    lstTableDongHo,
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
        public JsonResult ThemChiTietAnh(Int64? idDongHo)
        {
            try
            {
                var lstAnh = _en.Documents.AsEnumerable().Where(c => c.IdDongHo == idDongHo).Select(c => new
                {
                    c.UrlAnh,
                    c.TenFile,
                    c.IdDocument,
                }).ToList();

                return Json(new
                {
                    lstAnh,
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
        public JsonResult LoadFile(Int64? idDongHo)
        {
            try
            {
                var lstAnh = _en.Documents.AsEnumerable().Where(c => c.IdDongHo == idDongHo).Select(c => new
                {
                    c.UrlAnh,
                    c.TenFile,
                    c.IdDocument,
                }).ToList();

                return Json(new
                {
                    lstAnh,
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
        public JsonResult LoadDetail(Int64? idDongHo)
        {
            try
            {
                var data = _en.ChiTietDongHos.AsEnumerable().Where(c => idDongHo == c.IdDongHo).Select(c => new
                {
                    c.IdDongHo,
                    c.IdLoaiDongHo,
                    c.MaCode,
                    c.Sale,
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
                    c.GiaBan,
                    c.BaoHanh,
                    DtNgayTao = c.DtNgayTao != null ? string.Format("{0:yyyy-MM-dd}", c.DtNgayTao) : "",
                    UrlAnh = c.UrlAnh == "/ImageDongHo/" ? "" : c.UrlAnh,
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

        // Xóa bản ghi được chọn
        [HttpPost]
        public IActionResult DeleteData(Int64? idDongHo)
        {
            try
            {
                var _data = _en.ChiTietDongHos.Where(c => c.IdDongHo == idDongHo).FirstOrDefault();
                if (_data != null)
                {
                    _en.ChiTietDongHos.Remove(_data);
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

        [HttpPost]
        public IActionResult DeleteAnhCon(Int64? IdDocument)
        {
            try
            {
                var _data = _en.Documents.Where(c => c.IdDocument == IdDocument).FirstOrDefault();
                if (_data != null)
                {
                    _en.Documents.Remove(_data);
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
        public string GetActualpath(string FileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\ImageDongHo", FileName);
        }

    }
}
