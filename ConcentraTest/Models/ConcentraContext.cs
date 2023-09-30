using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ConcentraTest.Models;

public partial class ConcentraContext : DbContext
{
    public IConfigurationRoot Configuration { get; set; }
    public ConcentraContext()
    {

    }

    public ConcentraContext(DbContextOptions<ConcentraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<Plate> Plates { get; set; }

    public virtual DbSet<PlateRecord> PlateRecords { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            optionsBuilder.UseSqlServer(Configuration["Connectionstrings:connection"]);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Brand1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Brand");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Wdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.Status).WithMany(p => p.Brands)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brands_Status");

            entity.HasOne(d => d.User).WithMany(p => p.Brands)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brands_Users");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.ClientId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("ClientID");
            entity.Property(e => e.Birthdate).HasColumnType("date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Wdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.PersonType).WithMany(p => p.Clients)
                .HasForeignKey(d => d.PersonTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clients_PersonTypes");

            entity.HasOne(d => d.Status).WithMany(p => p.Clients)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clients_Status");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clients_Users");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Model1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Model");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Wdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Models_Brands");

            entity.HasOne(d => d.Status).WithMany(p => p.Models)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Models_Status");

            entity.HasOne(d => d.User).WithMany(p => p.Models)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Models_Users");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");
            entity.Property(e => e.PersonType1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PersonType");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Wdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.Status).WithMany(p => p.PersonTypes)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_PersonTypes_Status");

            entity.HasOne(d => d.User).WithMany(p => p.PersonTypes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PersonTypes_Users");
        });

        modelBuilder.Entity<Plate>(entity =>
        {
            entity.HasKey(e => e.PlateId).HasName("PK_Plates_1");

            entity.HasIndex(e => e.VehicleId).IsUnique();

            entity.Property(e => e.PlateId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PlateID");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Plates)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plates_Vehicles");
        });

        modelBuilder.Entity<PlateRecord>(entity =>
        {
            entity.Property(e => e.PlateRecordId).HasColumnName("PlateRecordID");
            entity.Property(e => e.PlateId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PlateID");
            entity.Property(e => e.PlateValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Wdate)
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.Plate).WithMany(p => p.PlateRecords)
                .HasPrincipalKey(p => p.PlateId)
                .HasForeignKey(d => d.PlateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlateRecords_Plates");

            entity.HasOne(d => d.Status).WithMany(p => p.PlateRecords)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlateRecords_Status");

            entity.HasOne(d => d.User).WithMany(p => p.PlateRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlateRecords_Users");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Status1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.ClientId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("ClientID");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VehicleTypeId).HasColumnName("VehicleTypeID");
            entity.Property(e => e.Wdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.Brand).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Brands");

            entity.HasOne(d => d.Client).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Clients");

            entity.HasOne(d => d.Model).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Models");

            entity.HasOne(d => d.Status).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Status");

            entity.HasOne(d => d.User).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Users");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_VehicleTypes");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.Property(e => e.VehicleTypeId).HasColumnName("VehicleTypeID");
            entity.Property(e => e.PlatePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PlateType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VehicleType1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VehicleType");
            entity.Property(e => e.Wdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("WDate");

            entity.HasOne(d => d.Status).WithMany(p => p.VehicleTypes)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleTypes_Status");

            entity.HasOne(d => d.User).WithMany(p => p.VehicleTypes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleTypes_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
