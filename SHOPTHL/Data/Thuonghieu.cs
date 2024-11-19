using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Thuonghieu
{
    public int Math { get; set; }

    public string Tenth { get; set; } = null!;

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
