using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class DAnhMucHeThong
{
    public long IdDanhMuc { get; set; }

    public string? MaDanhMuc { get; set; }

    public string? TenDanhMuc { get; set; }

    public string? CbLoaiDanhMuc { get; set; }
}
