using System.Diagnostics;
using IisReader.Helpers;
using IisReader.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IisReader.Data;

public partial class IisdbContext : DbContext
{
    public IisdbContext()
    {
    }

    public IisdbContext(DbContextOptions<IisdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataRaw> DataRaws { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<SysProp> SysProps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var _host = EnvironmentHelper.GetEnvirovmentVariable("CLIENT_HOST");
        var _username = EnvironmentHelper.GetEnvirovmentVariable("CLIENT_USERNAME");
        var _pswd = EnvironmentHelper.GetEnvirovmentVariable("CLIENT_PSWD");
        var _database = EnvironmentHelper.GetEnvirovmentVariable("CLIENT_DATABASE");

        var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = _host,
            Username = _username,
            Password = _pswd,
            Database = _database
        };

        optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString,
               options => options.EnableRetryOnFailure(maxRetryCount: 100));

        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataRaw>(entity =>
        {
            entity.HasKey(e => new { e.Layer, e.ArchiveItemid, e.SourceTime }).HasName("raw_data_idx_pk");

            entity.ToTable("data_raw");

            entity.HasIndex(e => e.SourceTime, "source_time");

            entity.Property(e => e.Layer).HasColumnName("layer");
            entity.Property(e => e.ArchiveItemid).HasColumnName("archive_itemid");
            entity.Property(e => e.SourceTime).HasColumnName("source_time");
            entity.Property(e => e.SValue).HasColumnName("s_value");
            entity.Property(e => e.ServerTime).HasColumnName("server_time");
            entity.Property(e => e.StatusCode).HasColumnName("status_code");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("itemsid_pk");

            entity.ToTable("items");

            entity.HasIndex(e => new { e.Itemid, e.Path, e.ProjectId }, "item_idx").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.FirstTime).HasColumnName("first_time");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.LastTime).HasColumnName("last_time");
            entity.Property(e => e.Name)
                .HasMaxLength(2000)
                .HasColumnName("name");
            entity.Property(e => e.Path)
                .HasMaxLength(2000)
                .HasColumnName("path");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projectsid_pk");

            entity.ToTable("projects");

            entity.HasIndex(e => e.Guid, "guid_idx").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Guid)
                .HasMaxLength(50)
                .HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SysProp>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("sys_propsname_pk");

            entity.ToTable("sys_props");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
