using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Dondathang
{
    public int Maddh { get; set; }

    public decimal? Tongtien { get; set; }

    public int? Makh { get; set; }

    public int? Maptvc { get; set; }

    public DateTime? Ngaydh { get; set; }

    public int? Matrangthai { get; set; }

    public string? Hoten { get; set; }

    public string? Diachi { get; set; }

    public string? Sdt { get; set; }

    public string? Lydohuy { get; set; }

    public virtual ICollection<Ctddh> Ctddhs { get; set; } = new List<Ctddh>();

    public virtual Khachhang? MakhNavigation { get; set; }

    public virtual Ptvanchuyen? MaptvcNavigation { get; set; }

    public virtual Trangthai? MatrangthaiNavigation { get; set; }
}
