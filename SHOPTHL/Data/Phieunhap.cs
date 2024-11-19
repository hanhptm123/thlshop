using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Phieunhap
{
    public int Mapn { get; set; }

    public DateTime? Ngaynhap { get; set; }

    public decimal? Tongtien { get; set; }

    public virtual ICollection<Ctphieunhap> Ctphieunhaps { get; set; } = new List<Ctphieunhap>();
}
