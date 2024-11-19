using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Ptthanhtoan
{
    public int Mapttt { get; set; }

    public string? Tenpttt { get; set; }

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
