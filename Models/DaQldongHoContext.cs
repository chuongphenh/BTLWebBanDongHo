using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBanDongHo.Models;

public partial class DaQldongHoContext : DbContext
{
    public DaQldongHoContext()
    {
    }

    public DaQldongHoContext(DbContextOptions<DaQldongHoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonDat> ChiTietDonDats { get; set; }

    public virtual DbSet<ChiTietDongHo> ChiTietDongHos { get; set; }

    public virtual DbSet<DAnhMucHeThong> DAnhMucHeThongs { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DonDatHang> DonDatHangs { get; set; }

    public virtual DbSet<LstLoaiDongHo> LstLoaiDongHos { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-80NUQEM\\MSSQLSERVER01;Initial Catalog=DA_QLDongHo;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonDat>(entity =>
        {
            entity.HasKey(e => e.IdChiTietDonDat);

            entity.ToTable("ChiTietDonDat");

            entity.Property(e => e.IsDuyet).HasColumnName("isDuyet");

            entity.HasOne(d => d.IdDonDatNavigation).WithMany(p => p.ChiTietDonDats)
                .HasForeignKey(d => d.IdDonDat)
                .HasConstraintName("FK_ChiTietDonDat_DonDatHang");
        });

        modelBuilder.Entity<ChiTietDongHo>(entity =>
        {
            entity.HasKey(e => e.IdDongHo);

            entity.ToTable("ChiTietDongHo");

            entity.Property(e => e.BaoHanh).HasMaxLength(150);
            entity.Property(e => e.CbChatLieuDay)
                .HasMaxLength(150)
                .HasColumnName("cbChatLieuDay");
            entity.Property(e => e.CbChatLieuMat)
                .HasMaxLength(50)
                .HasColumnName("cbChatLieuMat");
            entity.Property(e => e.CbChatLieuVo)
                .HasMaxLength(150)
                .HasColumnName("cbChatLieuVo");
            entity.Property(e => e.DtNgayCapNhap)
                .HasColumnType("date")
                .HasColumnName("dtNgayCapNhap");
            entity.Property(e => e.DtNgayTao)
                .HasColumnType("date")
                .HasColumnName("dtNgayTao");
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.KieuDang).HasMaxLength(150);
            entity.Property(e => e.MaCode)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.UrlAnh).HasColumnName("urlAnh");

            entity.HasOne(d => d.IdLoaiDongHoNavigation).WithMany(p => p.ChiTietDongHos)
                .HasForeignKey(d => d.IdLoaiDongHo)
                .HasConstraintName("FK_ChiTietDongHo_Lst_LoaiDongHo");
        });

        modelBuilder.Entity<DAnhMucHeThong>(entity =>
        {
            entity.HasKey(e => e.IdDanhMuc);

            entity.ToTable("D_anhMucHeThong");

            entity.Property(e => e.CbLoaiDanhMuc)
                .HasMaxLength(50)
                .HasColumnName("cbLoaiDanhMuc");
            entity.Property(e => e.MaDanhMuc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.IdDocument);

            entity.Property(e => e.TenFile)
                .HasMaxLength(100)
                .HasColumnName("tenFile");
            entity.Property(e => e.UrlAnh).HasColumnName("urlAnh");
        });

        modelBuilder.Entity<DonDatHang>(entity =>
        {
            entity.HasKey(e => e.IdDonDat);

            entity.ToTable("DonDatHang");

            entity.Property(e => e.DiaChi).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.HoTen).HasMaxLength(150);
            entity.Property(e => e.NgayLap).HasColumnType("date");
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TinhTrang).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.DonDatHangs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_DonDatHang_UserLogin");
        });

        modelBuilder.Entity<LstLoaiDongHo>(entity =>
        {
            entity.HasKey(e => e.IdLoaiDongHo);

            entity.ToTable("Lst_LoaiDongHo");

            entity.Property(e => e.HangSanXuat).HasMaxLength(250);
            entity.Property(e => e.IsTrangThai).HasColumnName("is_TrangThai");
            entity.Property(e => e.TenLoai).HasMaxLength(250);
            entity.Property(e => e.ThuongHieu).HasMaxLength(250);
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.IdThongBao);

            entity.ToTable("ThongBao");

            entity.Property(e => e.DuongDan).HasMaxLength(250);
            entity.Property(e => e.NoiDungGui).HasMaxLength(150);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserLogin");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(250);
            entity.Property(e => e.IsAdmin).HasColumnName("is_Admin");
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.UrlAnh).HasColumnName("urlAnh");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
