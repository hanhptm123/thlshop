using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Ctmau
{
    public string Mactmau { get; set; } = null!;

    public int Mamau { get; set; }

    public int Masp { get; set; }

    public string? Hinhanh { get; set; }

    public virtual ICollection<Ctsize> Ctsizes { get; set; } = new List<Ctsize>();

    public virtual Mau MamauNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
