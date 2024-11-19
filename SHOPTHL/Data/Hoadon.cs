using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Hoadon
{
    public int Mahd { get; set; }

    public DateTime? Ngaythanhtoan { get; set; }

    public decimal? Tongtien { get; set; }

    public int? Manv { get; set; }

    public int? Maptvc { get; set; }

    public int? Maptt { get; set; }

    public virtual ICollection<Cthoadon> Cthoadons { get; set; } = new List<Cthoadon>();

    public virtual Nhanvien? ManvNavigation { get; set; }

    public virtual Ptthanhtoan? MapttNavigation { get; set; }

    public virtual Ptvanchuyen? MaptvcNavigation { get; set; }
}
