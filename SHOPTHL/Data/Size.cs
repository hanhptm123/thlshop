using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Size
{
    public int Masize { get; set; }

    public string Tensize { get; set; } = null!;

    public virtual ICollection<Ctsize> Ctsizes { get; set; } = new List<Ctsize>();
}
