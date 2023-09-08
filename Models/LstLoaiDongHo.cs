using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class LstLoaiDongHo
{
    public long IdLoaiDongHo { get; set; }

    public string? TenLoai { get; set; }

    public string? HangSanXuat { get; set; }

    public string? ThuongHieu { get; set; }

    public bool? IsTrangThai { get; set; }

    public virtual ICollection<ChiTietDongHo> ChiTietDongHos { get; set; } = new List<ChiTietDongHo>();
}
