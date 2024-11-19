using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Nhanvien
{
    public int Manv { get; set; }

    public string? Tennv { get; set; }

    public string? Sdt { get; set; }

    public string? Diachi { get; set; }

    public string? Email { get; set; }

    public int? Mataikhoan { get; set; }

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual Taikhoan? MataikhoanNavigation { get; set; }
}
