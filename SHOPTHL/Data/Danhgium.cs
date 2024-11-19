using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Danhgium
{
    public int Madanhgia { get; set; }

    public int? Masp { get; set; }

    public int? Makh { get; set; }

    public DateTime? Ngaydanhgia { get; set; }

    public string? Loidanhgia { get; set; }

    public int? Diemdanhgia { get; set; }

    public string? Hinhanh { get; set; }

    public virtual Khachhang? MakhNavigation { get; set; }

    public virtual Sanpham? MaspNavigation { get; set; }
}
