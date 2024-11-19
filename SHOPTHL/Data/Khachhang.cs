using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Khachhang
{
    public int Makh { get; set; }

    public string? Tenkh { get; set; }

    public string? Sdt { get; set; }

    public string? Diachi { get; set; }

    public string? Email { get; set; }

    public int? Mataikhoan { get; set; }

    public virtual ICollection<Danhgium> Danhgia { get; set; } = new List<Danhgium>();

    public virtual ICollection<Diachi> Diachis { get; set; } = new List<Diachi>();

    public virtual ICollection<Dondathang> Dondathangs { get; set; } = new List<Dondathang>();

    public virtual Taikhoan? MataikhoanNavigation { get; set; }
}
