using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class ThongBao
{
    public long IdThongBao { get; set; }

    public long? UserIdGui { get; set; }

    public long? UserIdNhan { get; set; }

    public string? NoiDungGui { get; set; }

    public string? DuongDan { get; set; }
}
