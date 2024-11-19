using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Sanpham
{
    public int Masp { get; set; }

    public string Tensp { get; set; } = null!;

    public decimal Dongia { get; set; }

    public int? Maloai { get; set; }

    public string? Mota { get; set; }

    public int? Magiamgia { get; set; }

    public int? Math { get; set; }

    public string? Hinhanh { get; set; }

    public int? Mancc { get; set; }

    public string? Hinhanh1 { get; set; }

    public string? Hinhanh2 { get; set; }

    public string? Hinhanh3 { get; set; }

    public string? Bangsize { get; set; }

    public virtual ICollection<Ctddh> Ctddhs { get; set; } = new List<Ctddh>();

    public virtual ICollection<Cthoadon> Cthoadons { get; set; } = new List<Cthoadon>();

    public virtual ICollection<Ctmau> Ctmaus { get; set; } = new List<Ctmau>();

    public virtual ICollection<Ctmauu> Ctmauus { get; set; } = new List<Ctmauu>();

    public virtual ICollection<Ctphieunhap> Ctphieunhaps { get; set; } = new List<Ctphieunhap>();

    public virtual ICollection<Danhgium> Danhgia { get; set; } = new List<Danhgium>();

    public virtual Giamgium? MagiamgiaNavigation { get; set; }

    public virtual Loai? MaloaiNavigation { get; set; }

    public virtual Nhacungcap? ManccNavigation { get; set; }

    public virtual Thuonghieu? MathNavigation { get; set; }

}
