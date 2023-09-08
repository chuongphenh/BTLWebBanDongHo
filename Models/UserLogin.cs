using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class UserLogin
{
    public long UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? GioiTinh { get; set; }

    public string? UrlAnh { get; set; }

    public string? HoTen { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public DateTime? NgaySinh { get; set; }

    public bool? IsAdmin { get; set; }

    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
}
