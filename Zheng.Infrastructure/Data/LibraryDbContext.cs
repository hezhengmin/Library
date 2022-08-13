using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zheng.Infrastructure.Models;

#nullable disable

namespace Zheng.Infrastructure.Data
{
    public partial class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookPhoto> BookPhotos { get; set; }
        public virtual DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("帳號");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("信箱");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength(true)
                    .HasComment("密碼");

                entity.Property(e => e.SystemDate)
                    .HasColumnType("datetime")
                    .HasComment("系統日期");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("作者");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增者");

                entity.Property(e => e.CreatedBy).HasComment("新增時間");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Status).HasComment("狀態");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("書名標題");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改者");

                entity.Property(e => e.UpdatedBy).HasComment("修改時間");
            });

            modelBuilder.Entity<BookPhoto>(entity =>
            {
                entity.ToTable("BookPhoto");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.BookId).HasComment("書籍Id");

                entity.Property(e => e.FileId).HasComment("檔案Id");

                entity.Property(e => e.SystemDate)
                    .HasColumnType("datetime")
                    .HasComment("系統日期");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookPhotos)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookPhoto_Book");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("檔案類型");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增者");

                entity.Property(e => e.CreatedBy).HasComment("新增時間");

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("副檔名");

                entity.Property(e => e.Length).HasComment("檔案大小");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("檔案路徑");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改者");

                entity.Property(e => e.UpdatedBy).HasComment("修改時間");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
