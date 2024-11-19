using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Trangthai
{
    public int Matrangthai { get; set; }

    public string Tentrangthai { get; set; } = null!;

    public virtual ICollection<Dondathang> Dondathangs { get; set; } = new List<Dondathang>();
}
