using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Mau
{
    public int Mamau { get; set; }

    public string Tenmau { get; set; } = null!;

    public virtual ICollection<Ctmau> Ctmaus { get; set; } = new List<Ctmau>();

    public virtual ICollection<Ctmauu> Ctmauus { get; set; } = new List<Ctmauu>();
}
