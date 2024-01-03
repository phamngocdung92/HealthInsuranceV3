using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceV3.Entities;

public partial class HealthInsuranceContext : DbContext
{
    public HealthInsuranceContext()
    {
    }

    public HealthInsuranceContext(DbContextOptions<HealthInsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentManager> DepartmentManagers { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    public virtual DbSet<InsurancePackage> InsurancePackages { get; set; }

    public virtual DbSet<InsuranceRegistration> InsuranceRegistrations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<RejectionReason> RejectionReasons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TALKINGTREE;Initial Catalog=HealthInsurance;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD0D110F25");

            entity.ToTable("Department");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<DepartmentManager>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Departme__7AD04F11D8F955A6");

            entity.ToTable("DepartmentManager");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.ManagerId).HasMaxLength(450);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Department).WithMany(p => p.DepartmentManagers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Departmen__Depar__534D60F1");

            entity.HasOne(d => d.Manager).WithMany(p => p.DepartmentManagers)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Departmen__Manag__52593CB8");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__Insuranc__74231BC4ECA979E1");

            entity.ToTable("Insurance");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.IconText).HasMaxLength(255);
            entity.Property(e => e.InsuranceName).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.Insurances)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Insurance_Company");
        });

        modelBuilder.Entity<InsuranceCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Insuranc__2D971CACB55813F0");

            entity.ToTable("InsuranceCompany");

            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.EstablishedYear).HasColumnType("date");
        });

        modelBuilder.Entity<InsurancePackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Insuranc__322035CCAF844F6F");

            entity.ToTable("InsurancePackage");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.PackageName).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Insurance).WithMany(p => p.InsurancePackages)
                .HasForeignKey(d => d.InsuranceId)
                .HasConstraintName("FK__Insurance__Insur__5CD6CB2B");
        });

        modelBuilder.Entity<InsuranceRegistration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Insuranc__6EF588107CE41C06");

            entity.ToTable("InsuranceRegistration");

            entity.Property(e => e.ApprovalDate).HasColumnType("date");
            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.EmployeeId).HasMaxLength(450);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.RegistrationStatus).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.InsuranceRegistrations)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Insurance__Emplo__6383C8BA");

            entity.HasOne(d => d.Insurance).WithMany(p => p.InsuranceRegistrations)
                .HasForeignKey(d => d.InsuranceId)
                .HasConstraintName("FK__Insurance__Insur__6477ECF3");

            entity.HasOne(d => d.RejectionReason).WithMany(p => p.InsuranceRegistrations)
                .HasForeignKey(d => d.RejectionReasonId)
                .HasConstraintName("FK__Insurance__Rejec__656C112C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38ABD92272");

            entity.ToTable("Payment");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate).HasColumnType("date");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentStatusId)
                .HasConstraintName("FK__Payment__Payment__75A278F5");

            entity.HasOne(d => d.Registration).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__Payment__Registr__74AE54BC");
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId).HasName("PK__PaymentS__34F8AC1FAC9AE259");

            entity.ToTable("PaymentStatus");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<RejectionReason>(entity =>
        {
            entity.HasKey(e => e.ReasonId).HasName("PK__Rejectio__A4F8C0C7428E6E7B");

            entity.ToTable("RejectionReason");

            entity.Property(e => e.Dbstatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DBStatus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
