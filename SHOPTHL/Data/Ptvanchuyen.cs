using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Ptvanchuyen
{
    public int Maptvc { get; set; }

    public string? Tenptvc { get; set; }

    public decimal? Phivc { get; set; }

    public virtual ICollection<Dondathang> Dondathangs { get; set; } = new List<Dondathang>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
