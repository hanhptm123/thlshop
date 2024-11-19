using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Nhacungcap
{
    public int Mancc { get; set; }

    public string? Tenncc { get; set; }

    public string? Sdt { get; set; }

    public string? Diachi { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
