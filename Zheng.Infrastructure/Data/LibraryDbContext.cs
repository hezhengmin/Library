using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zheng.Infra.Data.Models;

#nullable disable

namespace Zheng.Infra.Data.Data
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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<UploadFile> UploadFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-UR67AEH0\\SQLEXPRESS;Initial Catalog=Library;User ID=sa;Password=123456");
            }
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

                entity.Property(e => e.Status).HasComment("狀態(1啟用0停用)");

                entity.Property(e => e.SystemDate)
                    .HasColumnType("datetime")
                    .HasComment("系統日期");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("帳號");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.Attachment)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("附件");

                entity.Property(e => e.Authority)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("授權資訊");

                entity.Property(e => e.Binding)
                    .HasMaxLength(10)
                    .HasComment("裝訂");

                entity.Property(e => e.Catalog)
                    .HasMaxLength(4000)
                    .HasComment("目次");

                entity.Property(e => e.CeasedDate)
                    .HasColumnType("datetime")
                    .HasComment("停刊註記");

                entity.Property(e => e.Classify)
                    .HasMaxLength(50)
                    .HasComment("書籍分類");

                entity.Property(e => e.Cover)
                    .HasMaxLength(200)
                    .HasComment("書封連結");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增時間");

                entity.Property(e => e.CreatedBy).HasComment("新增者");

                entity.Property(e => e.Creator)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("作者資訊");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("播放時間長度");

                entity.Property(e => e.Edition)
                    .HasMaxLength(50)
                    .HasComment("版次");

                entity.Property(e => e.Governance)
                    .HasMaxLength(50)
                    .HasComment("施政分類");

                entity.Property(e => e.Gpn)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("GPN")
                    .HasComment("GPN(政府出版品統一編號)");

                entity.Property(e => e.Gpntype)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("GPNType")
                    .HasComment("出版品分類");

                entity.Property(e => e.Grade)
                    .HasMaxLength(10)
                    .HasComment("級別");

                entity.Property(e => e.Introduction)
                    .HasMaxLength(4000)
                    .HasComment("abstract(書籍介紹)");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Issn)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ISSN");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("語言");

                entity.Property(e => e.NumberOfCopies).HasComment("庫存數量");

                entity.Property(e => e.Numbers)
                    .HasMaxLength(50)
                    .HasComment("字號");

                entity.Property(e => e.Pages).HasComment("頁數");

                entity.Property(e => e.Price).HasComment("價格");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("datetime")
                    .HasComment("出版日期");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("出版單位");

                entity.Property(e => e.Restriction)
                    .HasMaxLength(10)
                    .HasComment("權利範圍");

                entity.Property(e => e.RightCondition)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("出版情況");

                entity.Property(e => e.Size)
                    .HasMaxLength(50)
                    .HasComment("開數");

                entity.Property(e => e.Status).HasComment("狀態(0有庫存1無庫存)");

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .HasComment("主題分類");

                entity.Property(e => e.TargetPeople)
                    .HasMaxLength(50)
                    .HasComment("適用對象");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("書名標題");

                entity.Property(e => e.Types)
                    .HasMaxLength(50)
                    .HasComment("資料類型");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改時間");

                entity.Property(e => e.UpdatedBy).HasComment("修改者");

                entity.Property(e => e.Url)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasComment("出版品網址-線上版或試閱版");
            });

            modelBuilder.Entity<BookPhoto>(entity =>
            {
                entity.ToTable("BookPhoto");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.BookId).HasComment("書籍Id");

                entity.Property(e => e.SystemDate)
                    .HasColumnType("datetime")
                    .HasComment("系統日期");

                entity.Property(e => e.UploadFileId).HasComment("檔案Id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookPhotos)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookPhoto_Book");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增時間");

                entity.Property(e => e.CreatedBy).HasComment("新增者");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("名稱");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改時間");

                entity.Property(e => e.UpdatedBy).HasComment("修改者");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增時間");

                entity.Property(e => e.CreatedBy).HasComment("新增者");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasComment("借出結束日期");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime")
                    .HasComment("借出開始日期");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasComment("書籍歸還日期");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改時間");

                entity.Property(e => e.UpdatedBy).HasComment("修改者");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loan_Account");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loan_Book");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("系統編號");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasComment("JWT Token");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增時間");

                entity.Property(e => e.CreatedBy).HasComment("新增者");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasComment("Refresh Token到期日");

                entity.Property(e => e.IsRevorked).HasComment("是否出於安全原因已將其撤銷");

                entity.Property(e => e.IsUsed).HasComment("不想使用相同的 refresh token 生成新的 JWT token");

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasComment("Refresh Token");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改時間");

                entity.Property(e => e.UpdatedBy).HasComment("修改者");
            });

            modelBuilder.Entity<UploadFile>(entity =>
            {
                entity.ToTable("UploadFile");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("系統編號");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("檔案類型");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("新增時間");

                entity.Property(e => e.CreatedBy).HasComment("新增者");

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

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("修改時間");

                entity.Property(e => e.UpdatedBy).HasComment("修改者");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
