using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using WebBanDongHo.Models;

namespace WebBanDongHo.Controllers
{
    public class RegesterClientController : Controller
    {
        public IActionResult RegesterClient()
        {
            return View();
        }
        DaQldongHoContext _en = new DaQldongHoContext();

        [HttpPost]
        public JsonResult SaveData(string str_JSON)
        {
            try
            {

                var ClientData = JsonConvert.DeserializeObject<UserLogin>(str_JSON);
                if (ClientData.UserId == 0)
                {
                    var countTk = _en.UserLogins.Where(c => c.UserName == ClientData.UserName).Count();
                    if (countTk > 0)
                    {
                        return Json(new
                        {
                            message = "Tài khoản đã tồn tại",
                            status = false
                        });
                    }
                    ClientData.Password = Encrypt(ClientData.Password);
                    ClientData.IsAdmin = false;
                    _en.UserLogins.Add(ClientData);
                    _en.SaveChanges();
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
