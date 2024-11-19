using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Giamgium
{
    public int Magiamgia { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public int? Phantramgiam { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
