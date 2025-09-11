using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SendAttachmentsBySecureEmail9.Data.Models;

public partial class SimplifyVbcAdt8Context : DbContext
{
    public SimplifyVbcAdt8Context(DbContextOptions<SimplifyVbcAdt8Context> options)
        : base(options)
    {
        string projectPath = AppDomain.CurrentDomain.BaseDirectory;
        IConfigurationRoot configuration =
            new ConfigurationBuilder()
                .SetBasePath(projectPath)
        .AddJsonFile(MyConstants.AppSettingsFile)
        .Build();
        Database.SetCommandTimeout(9000);
        MyConnectionString =
            configuration.GetConnectionString(MyConstants.ConnectionString);
    }

    public string MyConnectionString { get; set; }

    public virtual DbSet<HumanaCensusAdtMaster> HumanaCensusAdtMasters { get; set; }
    public virtual DbSet<qy_GetSendAttachmentsBySecureEmailConfigOutputColumns> qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<qy_GetSendAttachmentsBySecureEmailConfigOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<HumanaCensusAdtMaster>(entity =>
        {
            entity.HasKey(e => e.HumanaCensusAdtMasterId).HasName("pk_dboHumanaCensusAdtMaster");

            entity.ToTable("HumanaCensusAdtMaster");

            entity.Property(e => e.HumanaCensusAdtMasterId).HasColumnName("HumanaCensusAdtMasterID");
            entity.Property(e => e.AdmitDate).HasColumnType("datetime");
            entity.Property(e => e.AuthStatus).HasMaxLength(300);
            entity.Property(e => e.Category).HasMaxLength(300);
            entity.Property(e => e.DateOfBirth).HasMaxLength(300);
            entity.Property(e => e.DischargeDate).HasColumnType("datetime");
            entity.Property(e => e.Disposition).HasMaxLength(300);
            entity.Property(e => e.DxDescription).HasMaxLength(300);
            entity.Property(e => e.FacilityOrPractitioner).HasMaxLength(300);
            entity.Property(e => e.NewYorN)
                .HasMaxLength(1)
                .HasColumnName("NewYOrN");
            entity.Property(e => e.PaneledProvider).HasMaxLength(300);
            entity.Property(e => e.PatEligible).HasMaxLength(300);
            entity.Property(e => e.PatEligibleThrough).HasMaxLength(300);
            entity.Property(e => e.PatienFirstName).HasMaxLength(300);
            entity.Property(e => e.PatientLastName).HasMaxLength(300);
            entity.Property(e => e.PatientName).HasMaxLength(300);
            entity.Property(e => e.Readmit).HasMaxLength(300);
            entity.Property(e => e.ReadmitRisk).HasMaxLength(300);
            entity.Property(e => e.SdohDetails).HasMaxLength(300);
            entity.Property(e => e.SrfDetails).HasMaxLength(300);
            entity.Property(e => e.SrfFlagYorN)
                .HasMaxLength(1)
                .HasColumnName("SrfFlagYOrN");
            entity.Property(e => e.StayType).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
