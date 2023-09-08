using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class DonDatHang
{
    public long IdDonDat { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? GhiChu { get; set; }

    public string? TinhTrang { get; set; }

    public string? HoTen { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public long? UserId { get; set; }

    public bool? IsGui { get; set; }

    public virtual ICollection<ChiTietDonDat> ChiTietDonDats { get; set; } = new List<ChiTietDonDat>();

    public virtual UserLogin? User { get; set; }
}
