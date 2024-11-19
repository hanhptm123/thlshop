using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Binhluan
{
    public int Mabinhluan { get; set; }

    public int? Mactddh { get; set; }

    public int? Diemdanhgia { get; set; }

    public string? Noidung { get; set; }

    public DateTime? Ngaybinhluan { get; set; }

    public string? Hinhanh { get; set; }

    public virtual Ctddh? MactddhNavigation { get; set; }
}
