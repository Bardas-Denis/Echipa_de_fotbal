using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Echipa_de_fotbal.Models;

public partial class EchipaDeFotbalContext : DbContext
{
    public EchipaDeFotbalContext()
    {
    }

    public EchipaDeFotbalContext(DbContextOptions<EchipaDeFotbalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Angajat> Employees { get; set; }

    public virtual DbSet<Campionat> Championships { get; set; }

    public virtual DbSet<ContractAngajat> EmployeesContracts { get; set; }

    public virtual DbSet<ContractJucator> PlayersContracts { get; set; }

    public virtual DbSet<Echipa> Teams { get; set; }

    public virtual DbSet<Jucator> Players { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    public virtual DbSet<SponsorEchipa> TeamsSponsors { get; set; }

    public virtual DbSet<SponsorJucator> PlayersSponsors { get; set; }

    public virtual DbSet<Stadion> Stadiums { get; set; }

    static readonly IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
    readonly string ?connectionString = configuration.GetConnectionString("MyConnectionString");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.Entity<Angajat>(entity =>
        {
            entity.HasKey(e => e.IdAngajat).HasName("pk__angajat__e09dcaf423939225");

            entity.ToTable("angajat", "dbo");

            entity.Property(e => e.IdAngajat)
                .ValueGeneratedNever()
                .HasColumnName("idangajat");
            entity.Property(e => e.DataN).HasColumnName("datan");
            entity.Property(e => e.Functie)
                .HasMaxLength(50)
                .HasColumnName("functie");
            entity.Property(e => e.IdEchipa).HasColumnName("idechipa");
            entity.Property(e => e.NivelStudii)
                .HasMaxLength(50)
                .HasColumnName("nivelstudii");
            entity.Property(e => e.Nume)
                .HasMaxLength(50)
                .HasColumnName("nume");
            entity.Property(e => e.Prenume)
                .HasMaxLength(50)
                .HasColumnName("prenume");

            entity.HasOne(d => d.IdEchipaNavigation).WithMany(p => p.Angajati)
                .HasForeignKey(d => d.IdEchipa)
                .HasConstraintName("fk__angajat__idechip__37a5467c");
        });

        modelBuilder.Entity<Campionat>(entity =>
        {
            entity.HasKey(e => e.IdCampionat).HasName("pk__campiona__04f7e09ba4b2d059");

            entity.ToTable("campionat", "dbo");

            entity.Property(e => e.IdCampionat)
                .ValueGeneratedNever()
                .HasColumnName("idcampionat");
            entity.Property(e => e.Divizie).HasColumnName("divizie");
            entity.Property(e => e.NumeCampionat)
                .HasMaxLength(50)
                .HasColumnName("numecampionat");
            entity.Property(e => e.Tara)
                .HasMaxLength(50)
                .HasColumnName("tara");
        });

        modelBuilder.Entity<ContractAngajat>(entity =>
        {
            entity.HasKey(e => e.IdContractAngajat).HasName("pk__contract__764965f3dfeccec1");

            entity.ToTable("contractangajat", "dbo");

            entity.Property(e => e.IdContractAngajat)
                .ValueGeneratedNever()
                .HasColumnName("idcontractangajat");
            entity.Property(e => e.ClauzaReziliere).HasColumnName("clauzareziliere");
            entity.Property(e => e.DataExpirare).HasColumnName("dataexpirare");
            entity.Property(e => e.DataSemnare).HasColumnName("datasemnare");
            entity.Property(e => e.IdAngajat).HasColumnName("idangajat");
            entity.Property(e => e.IdEchipa).HasColumnName("idechipa");
            entity.Property(e => e.Salariu).HasColumnName("salariu");

            entity.HasOne(d => d.IdAngajatNavigation).WithMany(p => p.ContractAngajati)
                .HasForeignKey(d => d.IdAngajat)
                .HasConstraintName("fk__contracta__idang__398d8eee");

            entity.HasOne(d => d.IdEchipaNavigation).WithMany(p => p.Contractangajati)
                .HasForeignKey(d => d.IdEchipa)
                .HasConstraintName("fk__contracta__idech__38996ab5");
        });

        modelBuilder.Entity<ContractJucator>(entity =>
        {
            entity.HasKey(e => e.IdContractJucator).HasName("pk__contract__ca852c6a9536a9ea");

            entity.ToTable("contractjucator", "dbo");

            entity.Property(e => e.IdContractJucator)
                .ValueGeneratedNever()
                .HasColumnName("idcontractjucator");
            entity.Property(e => e.ClauzaReziliere).HasColumnName("clauzareziliere");
            entity.Property(e => e.DataExpirare).HasColumnName("dataexpirare");
            entity.Property(e => e.DataSemnare).HasColumnName("datasemnare");
            entity.Property(e => e.IdEchipa).HasColumnName("idechipa");
            entity.Property(e => e.IdJucator).HasColumnName("idjucator");
            entity.Property(e => e.Salariu).HasColumnName("salariu");

            entity.HasOne(d => d.IdEchipaNavigation).WithMany(p => p.Contractjucatori)
                .HasForeignKey(d => d.IdEchipa)
                .HasConstraintName("fk__contractj__idech__4316f928");

            entity.HasOne(d => d.IdJucatorNavigation).WithMany(p => p.Contractjucatori)
                .HasForeignKey(d => d.IdJucator)
                .HasConstraintName("fk__contractj__idjuc__440b1d61");
        });

        modelBuilder.Entity<Echipa>(entity =>
        {
            entity.HasKey(e => e.IdEchipa).HasName("pk__echipa__36339d8588df9471");

            entity.ToTable("echipa", "dbo");

            entity.HasIndex(e => e.NumeEchipa, "nume_echipa").IsUnique();

            entity.Property(e => e.IdEchipa)
                .ValueGeneratedNever()
                .HasColumnName("idechipa");
            entity.Property(e => e.IdCampionat).HasColumnName("idcampionat");
            entity.Property(e => e.IdStadion).HasColumnName("idstadion");
            entity.Property(e => e.NumeEchipa)
                .HasMaxLength(50)
                .HasColumnName("numeechipa");
            entity.Property(e => e.NumePatron)
                .HasMaxLength(50)
                .HasColumnName("numepatron");

            entity.HasOne(d => d.IdCampionatNavigation).WithMany(p => p.Echipe)
                .HasForeignKey(d => d.IdCampionat)
                .HasConstraintName("fk__echipa__idcampio__35bcfe0a");

            entity.HasOne(d => d.IdStadionNavigation).WithMany(p => p.Echipe)
                .HasForeignKey(d => d.IdStadion)
                .HasConstraintName("fk__echipa__idstadio__36b12243");
        });

        modelBuilder.Entity<Jucator>(entity =>
        {
            entity.HasKey(e => e.IdJucator).HasName("pk__jucator__9ac77f77a590dc05");

            entity.ToTable("jucator", "dbo");

            entity.Property(e => e.IdJucator)
                .ValueGeneratedNever()
                .HasColumnName("idjucator");
            entity.Property(e => e.DataN).HasColumnName("datan");
            entity.Property(e => e.Greutate).HasColumnName("greutate");
            entity.Property(e => e.IdEchipa).HasColumnName("idechipa");
            entity.Property(e => e.Inaltime).HasColumnName("inaltime");
            entity.Property(e => e.Nume)
                .HasMaxLength(50)
                .HasColumnName("nume");
            entity.Property(e => e.Pozitie)
                .HasMaxLength(50)
                .HasColumnName("pozitie");
            entity.Property(e => e.Prenume)
                .HasMaxLength(50)
                .HasColumnName("prenume");

            entity.HasOne(d => d.IdEchipaNavigation).WithMany(p => p.Jucatori)
                .HasForeignKey(d => d.IdEchipa)
                .HasConstraintName("fk__jucator__idechip__3a81b327");
        });

        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.HasKey(e => e.IdSponsor).HasName("pk__sponsor__2d5a6d8b5471799c");

            entity.ToTable("sponsor", "dbo");

            entity.Property(e => e.IdSponsor)
                .ValueGeneratedNever()
                .HasColumnName("idsponsor");
            entity.Property(e => e.NumeSponsor)
                .HasMaxLength(50)
                .HasColumnName("numesponsor");
        });

        modelBuilder.Entity<SponsorEchipa>(entity =>
        {
            entity.HasKey(e => new { e.IdSponsor, e.IdEchipa }).HasName("pk__sponsore__6e395453add7d5aa");

            entity.ToTable("sponsorechipa", "dbo");

            entity.Property(e => e.IdSponsor).HasColumnName("idsponsor");
            entity.Property(e => e.IdEchipa).HasColumnName("idechipa");
            entity.Property(e => e.DataSemnare).HasColumnName("datasemnare");
            entity.Property(e => e.DurataContract).HasColumnName("duratacontract");
            entity.Property(e => e.SumaOferita).HasColumnName("sumaoferita");

            entity.HasOne(d => d.IdEchipaNavigation).WithMany(p => p.SponsorEchipe)
                .HasForeignKey(d => d.IdEchipa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__sponsorec__idech__46e78a0c");

            entity.HasOne(d => d.IdSponsorNavigation).WithMany(p => p.SponsorEchipe)
                .HasForeignKey(d => d.IdSponsor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__sponsorec__idspo__44ff419a");
        });

        modelBuilder.Entity<SponsorJucator>(entity =>
        {
            entity.HasKey(e => new { e.IdSponsor, e.IdJucator }).HasName("pk__sponsorj__44f61a7ca3df434a");

            entity.ToTable("sponsorjucator", "dbo");

            entity.Property(e => e.IdSponsor).HasColumnName("idsponsor");
            entity.Property(e => e.IdJucator).HasColumnName("idjucator");
            entity.Property(e => e.DataSemnare).HasColumnName("datasemnare");
            entity.Property(e => e.DurataContract).HasColumnName("duratacontract");
            entity.Property(e => e.SumaOferita).HasColumnName("sumaoferita");

            entity.HasOne(d => d.IdJucatorNavigation).WithMany(p => p.Sponsorjucatori)
                .HasForeignKey(d => d.IdJucator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__sponsorju__idjuc__47dbae45");

            entity.HasOne(d => d.IdSponsorNavigation).WithMany(p => p.SponsorJucatori)
                .HasForeignKey(d => d.IdSponsor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__sponsorju__idspo__45f365d3");
        });

        modelBuilder.Entity<Stadion>(entity =>
        {
            entity.HasKey(e => e.IdStadion).HasName("pk__stadion__5cb1dafc2dc3e3d4");

            entity.ToTable("stadion", "dbo");

            entity.Property(e => e.IdStadion)
                .ValueGeneratedNever()
                .HasColumnName("idstadion");
            entity.Property(e => e.Capacitate).HasColumnName("capacitate");
            entity.Property(e => e.NumeStadion)
                .HasMaxLength(50)
                .HasColumnName("numestadion");
            entity.Property(e => e.Oras)
                .HasMaxLength(50)
                .HasColumnName("oras");
            entity.Property(e => e.Strada)
                .HasMaxLength(50)
                .HasColumnName("strada");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
