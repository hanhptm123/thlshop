using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Ctsize
{
    public string Mactsize { get; set; } = null!;

    public string Mactmau { get; set; } = null!;

    public int Masize { get; set; }

    public int? Soluong { get; set; }

    public virtual Ctmau MactmauNavigation { get; set; } = null!;

    public virtual Size MasizeNavigation { get; set; } = null!;
}
