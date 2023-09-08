using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using WebBanDongHo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccessController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DashBoard", "Home");

            }
        }

        DaQldongHoContext _en = new DaQldongHoContext();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    string? PassWord = null;
                    if (userLogin.Password != null)
                    {
                        PassWord = Encrypt(userLogin.Password);
                    }
                    var data = _en.UserLogins.Where(c => c.UserName == userLogin.UserName && c.Password == PassWord).FirstOrDefault();

                    if (data != null)
                    {
                        if (data.IsAdmin == true)
                        {
                            HttpContext.Session.SetString("UserName", data.UserName);
                            return RedirectToAction("DashBoard", "Home");
                        }
                        else if (data.IsAdmin == false)
                        {
                            HttpContext.Session.SetString("UserName", data.UserName);
                            return RedirectToAction("DatHang", "DatHang");
                        }
                    }
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
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
