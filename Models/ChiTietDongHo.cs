using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class ChiTietDongHo
{
    public long IdDongHo { get; set; }

    public long? IdLoaiDongHo { get; set; }

    public string? MaCode { get; set; }

    public decimal? GiaBan { get; set; }

    public int? Sale { get; set; }

    public string? UrlAnh { get; set; }

    public int? SoLuong { get; set; }

    public string? KieuDang { get; set; }

    public double? DungLuong { get; set; }

    public bool? ChiuNuoc { get; set; }

    public string? CbChatLieuMat { get; set; }

    public int? DuongKinhMat { get; set; }

    public string? CbChatLieuDay { get; set; }

    public double? KichThuocDay { get; set; }

    public string? CbChatLieuVo { get; set; }

    public double? KichThuocVo { get; set; }

    public string? BaoHanh { get; set; }

    public bool? Active { get; set; }

    public DateTime? DtNgayTao { get; set; }

    public DateTime? DtNgayCapNhap { get; set; }

    public virtual LstLoaiDongHo? IdLoaiDongHoNavigation { get; set; }
}
