using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class Document
{
    public long IdDocument { get; set; }

    public string? TenFile { get; set; }

    public string? UrlAnh { get; set; }

    public long? IdDongHo { get; set; }
}
