using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Ctmauu
{
    public int Mactmau { get; set; }

    public int Masp { get; set; }

    public int Mamau { get; set; }

    public string? Hinhanh { get; set; }

    public virtual Mau MamauNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
