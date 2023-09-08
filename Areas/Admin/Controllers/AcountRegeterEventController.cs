using Microsoft.AspNetCore.Mvc;
using WebBanDongHo.Models.Authentication;
using WebBanDongHo.Models;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AcountRegeterEventController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        DaQldongHoContext _db = new DaQldongHoContext();

        [Authentication]
        public IActionResult AcountRegeter()
        {
            return View();
        }

        public AcountRegeterEventController(IWebHostEnvironment environment)
        {
            webHostEnvironment = environment;
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

                var ClientData = JsonConvert.DeserializeObject<UserLogin>(str_JSON);
                if (ClientData.UserId == 0)
                {
                    var countTk = _db.UserLogins.Where(c => c.UserName == ClientData.UserName).Count();
                    if (countTk > 0)
                    {
                        return Json(new
                        {
                            message = "Tài khoản đã tồn tại",
                            status = false
                        });
                    }

                    ClientData.UrlAnh = "/DocumentImage/" + fileName;
                    ClientData.Password = Encrypt(ClientData.Password);
                    _db.UserLogins.Add(ClientData);
                    _db.SaveChanges();
                }
                else
                {
                    //cập nhập
                    var ServerData = _db.UserLogins.Find(ClientData.UserId);
                    if (ServerData != null)
                    {
                        ServerData.UserName = ClientData.UserName;

                        if (Decrypt(ServerData.Password) != ClientData.Password)
                        {
                            ServerData.Password = Encrypt(ClientData.Password);
                        }
                        ServerData.GioiTinh = ClientData.GioiTinh;
                        ServerData.Email = ClientData.Email;
                        ServerData.NgaySinh = ClientData.NgaySinh;
                        ServerData.IsAdmin = ClientData.IsAdmin;

                        if (fileName != "")
                        {
                            ServerData.UrlAnh = "/DocumentImage/" + fileName;
                        }

                        _db.SaveChanges();
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

                throw;
            }

        }


        [HttpGet]
        public JsonResult LoadTable()
        {
            try
            {
                var data = _db.UserLogins.ToList();

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
        public JsonResult LoadDetail(Int64? UserId)
        {
            try
            {
                var data = _db.UserLogins.AsEnumerable().Where(c => UserId == c.UserId).Select(c => new
                {
                    c.UserId,
                    c.IsAdmin,
                    Password = Decrypt(c.Password),
                    c.UserName,
                    c.Email,
                    c.DiaChi,
                    c.SoDienThoai,
                    c.GioiTinh,
                    UrlAnh = c.UrlAnh == "/DocumentImage/" ? "" : c.UrlAnh,
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

        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";


        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public string GetActualpath(string FileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\DocumentImage", FileName);
        }


    }
}
