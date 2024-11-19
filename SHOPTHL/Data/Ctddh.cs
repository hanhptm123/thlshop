using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Ctddh
{
    public int Mactddh { get; set; }

    public int? Maddh { get; set; }

    public int? Masp { get; set; }

    public int? Soluong { get; set; }

    public decimal? Thanhtien { get; set; }

    public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

    public virtual Dondathang? MaddhNavigation { get; set; }

    public virtual Sanpham? MaspNavigation { get; set; }
}
