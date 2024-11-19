using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SHOPTHL.Data;

public partial class Thlshop2Context : DbContext
{
    public Thlshop2Context()
    {
    }

    public Thlshop2Context(DbContextOptions<Thlshop2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Baiviet> Baiviets { get; set; }

    public virtual DbSet<Binhluan> Binhluans { get; set; }

    public virtual DbSet<Ctddh> Ctddhs { get; set; }

    public virtual DbSet<Cthoadon> Cthoadons { get; set; }

    public virtual DbSet<Ctmau> Ctmaus { get; set; }

    public virtual DbSet<Ctmauu> Ctmauus { get; set; }

    public virtual DbSet<Ctphieunhap> Ctphieunhaps { get; set; }

    public virtual DbSet<Ctsize> Ctsizes { get; set; }

    public virtual DbSet<Danhgium> Danhgia { get; set; }

    public virtual DbSet<Diachi> Diachis { get; set; }

    public virtual DbSet<Dondathang> Dondathangs { get; set; }

    public virtual DbSet<Giamgium> Giamgia { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loai> Loais { get; set; }

    public virtual DbSet<Mau> Maus { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieunhap> Phieunhaps { get; set; }

    public virtual DbSet<Ptthanhtoan> Ptthanhtoans { get; set; }

    public virtual DbSet<Ptvanchuyen> Ptvanchuyens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Thuonghieu> Thuonghieus { get; set; }

    public virtual DbSet<Trangthai> Trangthais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MYHANH\\MYHANH;Initial Catalog=THLSHOP2;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baiviet>(entity =>
        {
            entity.HasKey(e => e.Mabaiviet).HasName("PK__BAIVIET__98A21B4B73B3FB6E");

            entity.ToTable("BAIVIET");

            entity.Property(e => e.Mabaiviet).HasColumnName("MABAIVIET");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaydang)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANG");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
            entity.Property(e => e.Tenbaiviet)
                .HasMaxLength(30)
                .HasColumnName("TENBAIVIET");
        });

        modelBuilder.Entity<Binhluan>(entity =>
        {
            entity.HasKey(e => e.Mabinhluan).HasName("PK__BINHLUAN__1D9D3DB0A10E34E8");

            entity.ToTable("BINHLUAN");

            entity.Property(e => e.Mabinhluan).HasColumnName("MABINHLUAN");
            entity.Property(e => e.Diemdanhgia).HasColumnName("DIEMDANHGIA");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Mactddh).HasColumnName("MACTDDH");
            entity.Property(e => e.Ngaybinhluan)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBINHLUAN");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");

            entity.HasOne(d => d.MactddhNavigation).WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.Mactddh)
                .HasConstraintName("FK_BINHLUAN_CTDDH");
        });

        modelBuilder.Entity<Ctddh>(entity =>
        {
            entity.HasKey(e => e.Mactddh).HasName("PK__CTDDH__BC9522357FA6724C");

            entity.ToTable("CTDDH");

            entity.Property(e => e.Mactddh).HasColumnName("MACTDDH");
            entity.Property(e => e.Maddh).HasColumnName("MADDH");
            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MaddhNavigation).WithMany(p => p.Ctddhs)
                .HasForeignKey(d => d.Maddh)
                .HasConstraintName("FK_CTDDH_DONDATHANG");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Ctddhs)
                .HasForeignKey(d => d.Masp)
                .HasConstraintName("FK_CTDDH_SANPHAM");
        });

        modelBuilder.Entity<Cthoadon>(entity =>
        {
            entity.HasKey(e => new { e.Masp, e.Mahd });

            entity.ToTable("CTHOADON");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Cthoadons)
                .HasForeignKey(d => d.Mahd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTHOADON_HOADON");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Cthoadons)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTHOADON_SANPHAM");
        });

        modelBuilder.Entity<Ctmau>(entity =>
        {
            entity.HasKey(e => e.Mactmau).HasName("PK__tmp_ms_x__B2F5B6DD51253480");

            entity.ToTable("CTMAU");

            entity.HasIndex(e => new { e.Mamau, e.Masp }, "UC_CTMAU").IsUnique();

            entity.Property(e => e.Mactmau)
                .HasMaxLength(50)
                .HasColumnName("MACTMAU");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Mamau).HasColumnName("MAMAU");
            entity.Property(e => e.Masp).HasColumnName("MASP");

            entity.HasOne(d => d.MamauNavigation).WithMany(p => p.Ctmaus)
                .HasForeignKey(d => d.Mamau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTMAU_MAU");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Ctmaus)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTMAU_SANPHAM");
        });

        modelBuilder.Entity<Ctmauu>(entity =>
        {
            entity.HasKey(e => new { e.Masp, e.Mamau }).HasName("PK_ctmauu");

            entity.ToTable("CTMAUU");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Mamau).HasColumnName("MAMAU");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Mactmau)
                .ValueGeneratedOnAdd()
                .HasColumnName("MACTMAU");

            entity.HasOne(d => d.MamauNavigation).WithMany(p => p.Ctmauus)
                .HasForeignKey(d => d.Mamau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTMAUU_MAU");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Ctmauus)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTMAUU_SANPHAM");
        });

        modelBuilder.Entity<Ctphieunhap>(entity =>
        {
            entity.HasKey(e => new { e.Masp, e.Mapn });

            entity.ToTable("CTPHIEUNHAP");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Mapn).HasColumnName("MAPN");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MapnNavigation).WithMany(p => p.Ctphieunhaps)
                .HasForeignKey(d => d.Mapn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPHIEUNHAP_PHIEUNHAP");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Ctphieunhaps)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPHIEUNHAP_SANPHAM");
        });

        modelBuilder.Entity<Ctsize>(entity =>
        {
            entity.HasKey(e => e.Mactsize).HasName("PK__tmp_ms_x__7B34BD1324CCB058");

            entity.ToTable("CTSIZE");

            entity.HasIndex(e => new { e.Mactmau, e.Masize }, "UC_CTSIZE").IsUnique();

            entity.Property(e => e.Mactsize)
                .HasMaxLength(50)
                .HasColumnName("MACTSIZE");
            entity.Property(e => e.Mactmau)
                .HasMaxLength(50)
                .HasColumnName("MACTMAU");
            entity.Property(e => e.Masize).HasColumnName("MASIZE");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");

            entity.HasOne(d => d.MactmauNavigation).WithMany(p => p.Ctsizes)
                .HasForeignKey(d => d.Mactmau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTSIZE_CTMAU");

            entity.HasOne(d => d.MasizeNavigation).WithMany(p => p.Ctsizes)
                .HasForeignKey(d => d.Masize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTSIZE_SIZE");
        });

        modelBuilder.Entity<Danhgium>(entity =>
        {
            entity.HasKey(e => e.Madanhgia).HasName("PK__DANHGIA__8597D60AFDF78B6B");

            entity.ToTable("DANHGIA");

            entity.Property(e => e.Madanhgia).HasColumnName("MADANHGIA");
            entity.Property(e => e.Diemdanhgia).HasColumnName("DIEMDANHGIA");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Loidanhgia).HasColumnName("LOIDANHGIA");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Ngaydanhgia)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANHGIA");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Danhgia)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_DANHGIA_KHACHHANG");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Danhgia)
                .HasForeignKey(d => d.Masp)
                .HasConstraintName("FK_DANHGIA_SANPHAM");
        });

        modelBuilder.Entity<Diachi>(entity =>
        {
            entity.HasKey(e => e.Madiachi).HasName("PK__DIACHI__B416C9CED7723A97");

            entity.ToTable("DIACHI");

            entity.Property(e => e.Madiachi).HasColumnName("MADIACHI");
            entity.Property(e => e.Diachi1)
                .HasMaxLength(30)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(30)
                .HasColumnName("SDT");
            entity.Property(e => e.Tennguoinhan)
                .HasMaxLength(30)
                .HasColumnName("TENNGUOINHAN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Diachis)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_DIACHI_KHACHHANG");
        });

        modelBuilder.Entity<Dondathang>(entity =>
        {
            entity.HasKey(e => e.Maddh).HasName("PK__DONDATHA__77CD19D15681378D");

            entity.ToTable("DONDATHANG");

            entity.Property(e => e.Maddh).HasColumnName("MADDH");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Lydohuy)
                .HasMaxLength(200)
                .HasColumnName("LYDOHUY");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Maptvc).HasColumnName("MAPTVC");
            entity.Property(e => e.Matrangthai).HasColumnName("MATRANGTHAI");
            entity.Property(e => e.Ngaydh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("TONGTIEN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_DONDATHANG_KHACHHANG");

            entity.HasOne(d => d.MaptvcNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Maptvc)
                .HasConstraintName("FK_DONDATHANG_PTVANCHUYEN");

            entity.HasOne(d => d.MatrangthaiNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Matrangthai)
                .HasConstraintName("FK_DONDATHANG_TRANGTHAI");
        });

        modelBuilder.Entity<Giamgium>(entity =>
        {
            entity.HasKey(e => e.Magiamgia).HasName("PK__GIAMGIA__41C2843974595B91");

            entity.ToTable("GIAMGIA");

            entity.Property(e => e.Magiamgia).HasColumnName("MAGIAMGIA");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKETTHUC");
            entity.Property(e => e.Phantramgiam).HasColumnName("PHANTRAMGIAM");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Mahd).HasName("PK__HOADON__603F20CE179CBEA2");

            entity.ToTable("HOADON");

            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Maptt).HasColumnName("MAPTT");
            entity.Property(e => e.Maptvc).HasColumnName("MAPTVC");
            entity.Property(e => e.Ngaythanhtoan)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHANHTOAN");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("TONGTIEN");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK_HOADON_NHANVIEN");

            entity.HasOne(d => d.MapttNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Maptt)
                .HasConstraintName("FK_HOADON_PTTHANHTOAN");

            entity.HasOne(d => d.MaptvcNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Maptvc)
                .HasConstraintName("FK_HOADON_PTVANCHUYEN");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Makh).HasName("PK__KHACHHAN__603F592CFAD93E7C");

            entity.ToTable("KHACHHANG");

            entity.HasIndex(e => e.Mataikhoan, "UC_KHACHHANG").IsUnique();

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Diachi)
                .HasMaxLength(30)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Mataikhoan).HasColumnName("MATAIKHOAN");
            entity.Property(e => e.Sdt)
                .HasMaxLength(30)
                .HasColumnName("SDT");
            entity.Property(e => e.Tenkh)
                .HasMaxLength(30)
                .HasColumnName("TENKH");

            entity.HasOne(d => d.MataikhoanNavigation).WithOne(p => p.Khachhang)
                .HasForeignKey<Khachhang>(d => d.Mataikhoan)
                .HasConstraintName("FK_KHACHHANG_TAIKHOAN");
        });

        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.Maloai).HasName("PK__LOAI__2F633F23712FEF97");

            entity.ToTable("LOAI");

            entity.HasIndex(e => e.Tenloai, "UC_LOAI").IsUnique();

            entity.Property(e => e.Maloai).HasColumnName("MALOAI");
            entity.Property(e => e.Tenloai)
                .HasMaxLength(30)
                .HasColumnName("TENLOAI");
        });

        modelBuilder.Entity<Mau>(entity =>
        {
            entity.HasKey(e => e.Mamau).HasName("PK__MAU__7B7346CF76FB5E09");

            entity.ToTable("MAU");

            entity.Property(e => e.Mamau).HasColumnName("MAMAU");
            entity.Property(e => e.Tenmau)
                .HasMaxLength(30)
                .HasColumnName("TENMAU");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.Mancc).HasName("PK__NHACUNGC__7ABEA5827F335B68");

            entity.ToTable("NHACUNGCAP");

            entity.Property(e => e.Mancc).HasColumnName("MANCC");
            entity.Property(e => e.Diachi)
                .HasMaxLength(30)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Sdt)
                .HasMaxLength(30)
                .HasColumnName("SDT");
            entity.Property(e => e.Tenncc)
                .HasMaxLength(30)
                .HasColumnName("TENNCC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("PK__NHANVIEN__603F51144C4BFCCE");

            entity.ToTable("NHANVIEN");

            entity.HasIndex(e => e.Mataikhoan, "UC_NHANVIEN").IsUnique();

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Diachi)
                .HasMaxLength(30)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Mataikhoan).HasColumnName("MATAIKHOAN");
            entity.Property(e => e.Sdt)
                .HasMaxLength(30)
                .HasColumnName("SDT");
            entity.Property(e => e.Tennv)
                .HasMaxLength(30)
                .HasColumnName("TENNV");

            entity.HasOne(d => d.MataikhoanNavigation).WithOne(p => p.Nhanvien)
                .HasForeignKey<Nhanvien>(d => d.Mataikhoan)
                .HasConstraintName("FK_NHANVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Phieunhap>(entity =>
        {
            entity.HasKey(e => e.Mapn).HasName("PK__PHIEUNHA__603F61CE8EE127E9");

            entity.ToTable("PHIEUNHAP");

            entity.Property(e => e.Mapn).HasColumnName("MAPN");
            entity.Property(e => e.Ngaynhap)
                .HasColumnType("datetime")
                .HasColumnName("NGAYNHAP");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("TONGTIEN");
        });

        modelBuilder.Entity<Ptthanhtoan>(entity =>
        {
            entity.HasKey(e => e.Mapttt).HasName("PK__PTTHANHT__4F6B743E4E44E671");

            entity.ToTable("PTTHANHTOAN");

            entity.Property(e => e.Mapttt).HasColumnName("MAPTTT");
            entity.Property(e => e.Tenpttt)
                .HasMaxLength(30)
                .HasColumnName("TENPTTT");
        });

        modelBuilder.Entity<Ptvanchuyen>(entity =>
        {
            entity.HasKey(e => e.Maptvc).HasName("PK__PTVANCHU__4F6B640D90C554C8");

            entity.ToTable("PTVANCHUYEN");

            entity.Property(e => e.Maptvc).HasColumnName("MAPTVC");
            entity.Property(e => e.Phivc)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("PHIVC");
            entity.Property(e => e.Tenptvc)
                .HasMaxLength(30)
                .HasColumnName("TENPTVC");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Masp).HasName("PK__SANPHAM__60228A3236F5AE31");

            entity.ToTable("SANPHAM");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Bangsize).HasColumnName("BANGSIZE");
            entity.Property(e => e.Dongia)
                .HasColumnType("decimal(8, 0)")
                .HasColumnName("DONGIA");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Hinhanh1).HasColumnName("HINHANH1");
            entity.Property(e => e.Hinhanh2).HasColumnName("HINHANH2");
            entity.Property(e => e.Hinhanh3).HasColumnName("HINHANH3");
            entity.Property(e => e.Magiamgia).HasColumnName("MAGIAMGIA");
            entity.Property(e => e.Maloai).HasColumnName("MALOAI");
            entity.Property(e => e.Mancc).HasColumnName("MANCC");
            entity.Property(e => e.Math).HasColumnName("MATH");
            entity.Property(e => e.Mota).HasColumnName("MOTA");
            entity.Property(e => e.Tensp)
                .HasMaxLength(30)
                .HasColumnName("TENSP");

            entity.HasOne(d => d.MagiamgiaNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Magiamgia)
                .HasConstraintName("FK_SANPHAM_GIAMGIA");

            entity.HasOne(d => d.MaloaiNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Maloai)
                .HasConstraintName("FK_SANPHAM_LOAI");

            entity.HasOne(d => d.ManccNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mancc)
                .HasConstraintName("FK_SANPHAM_NHACUNGCAP");

            entity.HasOne(d => d.MathNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Math)
                .HasConstraintName("FK_SANPHAM_THUONGHIEU");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Masize).HasName("PK__SIZE__3DD4402B89ED3FCB");

            entity.ToTable("SIZE");

            entity.Property(e => e.Masize).HasColumnName("MASIZE");
            entity.Property(e => e.Tensize)
                .HasMaxLength(30)
                .HasColumnName("TENSIZE");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Mataikhoan).HasName("PK__TAIKHOAN__2ED8B5179933B411");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.Tendangnhap, "UC_TAIKHOAN").IsUnique();

            entity.Property(e => e.Mataikhoan).HasColumnName("MATAIKHOAN");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(30)
                .HasColumnName("CHUCVU");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(30)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Tendangnhap)
                .HasMaxLength(30)
                .HasColumnName("TENDANGNHAP");
        });

        modelBuilder.Entity<Thuonghieu>(entity =>
        {
            entity.HasKey(e => e.Math).HasName("PK__THUONGHI__6023721B14CBDD6C");

            entity.ToTable("THUONGHIEU");

            entity.Property(e => e.Math).HasColumnName("MATH");
            entity.Property(e => e.Tenth)
                .HasMaxLength(30)
                .HasColumnName("TENTH");
        });

        modelBuilder.Entity<Trangthai>(entity =>
        {
            entity.HasKey(e => e.Matrangthai).HasName("PK__TRANGTHA__06B362F1215AB595");

            entity.ToTable("TRANGTHAI");

            entity.Property(e => e.Matrangthai).HasColumnName("MATRANGTHAI");
            entity.Property(e => e.Tentrangthai)
                .HasMaxLength(30)
                .HasColumnName("TENTRANGTHAI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
