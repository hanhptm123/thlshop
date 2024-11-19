using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Cthoadon
{
    public int Masp { get; set; }

    public int Mahd { get; set; }

    public int? Soluong { get; set; }

    public decimal? Thanhtien { get; set; }

    public virtual Hoadon MahdNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
