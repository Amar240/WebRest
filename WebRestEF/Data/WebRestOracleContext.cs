using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Models;

namespace WebRest.EF.Data;

public partial class WebRestOracleContext : DbContext
{
    public WebRestOracleContext(DbContextOptions<WebRestOracleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderState> OrderStates { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrdersLine> OrdersLines { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<ProductStatus> ProductStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("UD_AMARNATH")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("ADDRESS_PK");

            entity.Property(e => e.AddressId).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressState).IsFixedLength();
            entity.Property(e => e.AddressUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.AddressTypeId).HasName("ADDRESS_TYPE_PK");

            entity.Property(e => e.AddressTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressTypeCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressTypeCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressTypeUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressTypeUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("CUSTOMER_PK");

            entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CustomerGender).WithMany(p => p.Customers).HasConstraintName("CUSTOMER_FK1");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.CustomerAddressId).HasName("CUSTOMER_ADDRESS_PK");

            entity.Property(e => e.CustomerAddressId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CustomerAddressAddress).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ADDRESS_FK2");

            entity.HasOne(d => d.CustomerAddressAddressType).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ADDRESS_FK3");

            entity.HasOne(d => d.CustomerAddressCustomer).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ADDRESS_FK1");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("GENDER_PK");

            entity.Property(e => e.GenderId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sys_guid() ");
            entity.Property(e => e.GenderCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.GenderCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.GenderUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.GenderUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdersId).HasName("ORDERS_PK");

            entity.Property(e => e.OrdersId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.OrdersCustomer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDERS_FK1");
        });

        modelBuilder.Entity<OrderState>(entity =>
        {
            entity.HasKey(e => e.OrderStateId).HasName("ORDER_STATE_PK");

            entity.Property(e => e.OrderStateId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStateCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStateCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStateUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStateUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.OrderStateOrderStatus).WithMany(p => p.OrderStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_STATE_FK2");

            entity.HasOne(d => d.OrderStateOrders).WithMany(p => p.OrderStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_STATE_FK1");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("ORDER_STATUS_PK");

            entity.Property(e => e.OrderStatusId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.OrderStatusNextOrderStatus).WithMany(p => p.InverseOrderStatusNextOrderStatus).HasConstraintName("ORDER_STATUS_FK1");
        });

        modelBuilder.Entity<OrdersLine>(entity =>
        {
            entity.HasKey(e => e.OrdersLineId).HasName("ORDERS_LINE_PK");

            entity.Property(e => e.OrdersLineId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersLineCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersLineCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersLineUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersLineUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.OrdersLineOrders).WithMany(p => p.OrdersLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDERS_LINE_FK1");

            entity.HasOne(d => d.OrdersLineProduct).WithMany(p => p.OrdersLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDERS_LINE_FK2");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRODUCT_PK");

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("SYS_GUID()");
            entity.Property(e => e.ProductCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProductProductStatus).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCT_FK1");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.ProductPriceId).HasName("PRODUCT_PRICE_PK");

            entity.Property(e => e.ProductPriceId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProductPriceProduct).WithMany(p => p.ProductPrices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCT_PRICE_FK1");
        });

        modelBuilder.Entity<ProductStatus>(entity =>
        {
            entity.HasKey(e => e.ProductStatusId).HasName("PRODUCT_STATUS_PK");

            entity.Property(e => e.ProductStatusId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("SYS_GUID()");
            entity.Property(e => e.ProductStatusCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductStatusCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductStatusUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductStatusUpdtId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
