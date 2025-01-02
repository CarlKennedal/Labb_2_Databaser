using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb_2_Databaser.Models;

public partial class CarlKennedalLabbEttContext : DbContext
{
    public CarlKennedalLabbEttContext()
    {
    }

    public CarlKennedalLabbEttContext(DbContextOptions<CarlKennedalLabbEttContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<SellOrder> SellOrders { get; set; }

    public virtual DbSet<StockBalance> StockBalances { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MANGLISH;Initial Catalog=CarlKennedalLabbEtt;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC14E38F4099");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Nationality).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.ISBN);

            entity.Property(e => e.ISBN)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorID__3D5E1FD2");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Books__CategoryI__3E52440B");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BC64A1F93");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8D117039B");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657E4BA0D9CD40");

            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublisherName).HasMaxLength(255);

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Publishers)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Publishers__ISBN__4BAC3F29");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ISBN)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.PurchaseOrderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PurchaseOrderID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.IsbnNavigation).WithMany()
                .HasForeignKey(d => d.ISBN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseOr__ISBN__4D94879B");

            entity.HasOne(d => d.Publisher).WithMany()
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Publi__4E88ABD4");

            entity.HasOne(d => d.Store).WithMany()
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Store__4F7CD00D");
        });

        modelBuilder.Entity<SellOrder>(entity =>
        {
            entity.HasKey(e => e.SellOrderId).HasName("PK__SellOrde__A5E79F78C9F7923D");

            entity.Property(e => e.SellOrderId).HasColumnName("SellOrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ISBN)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.SellOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SellOrder__Custo__48CFD27E");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.SellOrders)
                .HasForeignKey(d => d.ISBN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SellOrders__ISBN__47DBAE45");

            entity.HasOne(d => d.Store).WithMany(p => p.SellOrders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SellOrder__Store__46E78A0C");
        });

        modelBuilder.Entity<StockBalance>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ISBN });

            entity.ToTable("StockBalance");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.ISBN)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.ISBN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockBalance_Books");

            entity.HasOne(d => d.Store).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockBalance_Stores");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E18B3AB749");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.StoreName).HasMaxLength(100);
        });

        modelBuilder.Entity<TitlesPerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlesPerAuthor");

            entity.Property(e => e.Name).HasMaxLength(201);
            entity.Property(e => e.StockValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Stock value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
