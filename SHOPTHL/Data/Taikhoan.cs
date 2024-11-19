using System;
using System.Collections.Generic;

namespace SHOPTHL.Data;

public partial class Taikhoan
{
    public int Mataikhoan { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Matkhau { get; set; }

    public string? Chucvu { get; set; }

    public virtual Khachhang? Khachhang { get; set; }

    public virtual Nhanvien? Nhanvien { get; set; }
}
