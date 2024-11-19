using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Loai
{
    public int Maloai { get; set; }

    public string Tenloai { get; set; } = null!;

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
