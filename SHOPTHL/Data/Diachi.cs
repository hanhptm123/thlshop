using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Diachi
{
    public int Madiachi { get; set; }

    public int? Makh { get; set; }

    public string? Tennguoinhan { get; set; }

    public string? Sdt { get; set; }

    public string? Diachi1 { get; set; }

    public virtual Khachhang? MakhNavigation { get; set; }
}
