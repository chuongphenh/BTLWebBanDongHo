using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using WebBanDongHo.Models;

namespace WebBanDongHo.Controllers
{
    public class LoginClientController : Controller
    {
        public IActionResult LoginClient()
        {
            return View();
        }

        DaQldongHoContext _en = new DaQldongHoContext();

        [HttpPost]
        public IActionResult Login(string UserName, string PassWord)
        {
            try
            {
                string pass = Encrypt(PassWord).ToString();
                var data = _en.UserLogins.AsEnumerable().Where(c => c.UserName == UserName && (c.Password.ToString() == pass)).FirstOrDefault();

                if (data != null)
                {
                    if (data.IsAdmin == true)
                    {
                        HttpContext.Session.SetString("UserName", UserName);
                        return Json(new
                        {
                            IsAdmin = true,
                            message = "Cập nhật thành công",
                            status = true
                        });
                    }
                    else if (data.IsAdmin == false)
                    {
                        HttpContext.Session.SetString("UserName", UserName);
                        return Json(new
                        {
                            IsAdmin = false,
                            message = "Cập nhật thành công",
                            status = true
                        });
                    }
                }

                return Json(new
                {
                    IsAdmin = false,
                    message = "Cập nhật thành công",
                    status = false
                });
            }
            catch (Exception)
            {

                throw;
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

    }
}
