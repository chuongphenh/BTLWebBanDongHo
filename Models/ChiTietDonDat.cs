using System;
using System.Collections.Generic;

namespace WebBanDongHo.Models;

public partial class ChiTietDonDat
{
    public long IdChiTietDonDat { get; set; }

    public long? IdDongHo { get; set; }

    public long? IdDonDat { get; set; }

    public int? SoLuong { get; set; }

    public bool? IsDuyet { get; set; }

    public virtual DonDatHang? IdDonDatNavigation { get; set; }
}
