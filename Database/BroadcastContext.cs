using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.Database;

public partial class BroadcastContext : DbContext
{
    public BroadcastContext()
    {
    }

    public BroadcastContext(DbContextOptions<BroadcastContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archivage> Archivages { get; set; }

    public virtual DbSet<BroadcastGroup> BroadcastGroups { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1H35QEL\\SQLEXPRESS;Database=Broadcast;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archivage>(entity =>
        {
            entity.HasKey(e => e.IdArchivage).HasName("Archivages_PK");

            entity.Property(e => e.IdArchivage).HasColumnName("id_archivage");
            entity.Property(e => e.TitreArchivage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titre_archivage");
        });

        modelBuilder.Entity<BroadcastGroup>(entity =>
        {
            entity.HasKey(e => e.IdBroadcast).HasName("BroadcastGroup_PK");

            entity.ToTable("BroadcastGroup");

            entity.Property(e => e.IdBroadcast).HasColumnName("id_broadcast");
            entity.Property(e => e.TitreBroadcast)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titre_broadcast");

            entity.HasMany(d => d.IdClients).WithMany(p => p.IdBroadcasts)
                .UsingEntity<Dictionary<string, object>>(
                    "Appartenir",
                    r => r.HasOne<Client>().WithMany()
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Appartenir_Clients0_FK"),
                    l => l.HasOne<BroadcastGroup>().WithMany()
                        .HasForeignKey("IdBroadcast")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Appartenir_BroadcastGroup_FK"),
                    j =>
                    {
                        j.HasKey("IdBroadcast", "IdClient").HasName("Appartenir_PK");
                        j.ToTable("Appartenir");
                    });
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Clients_PK");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.EmailClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email_client");
            entity.Property(e => e.FirstnameClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname_client");
            entity.Property(e => e.LastnameClient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname_client");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.IdMessage).HasName("Messages_PK");

            entity.Property(e => e.IdMessage).HasColumnName("id_message");
            entity.Property(e => e.DateEnvoieMessage)
                .HasColumnType("date")
                .HasColumnName("date_envoie_message");
            entity.Property(e => e.IdArchivage).HasColumnName("id_archivage");
            entity.Property(e => e.IdBroadcast).HasColumnName("id_broadcast");
            entity.Property(e => e.Messages)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("messages");

            entity.HasOne(d => d.IdArchivageNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdArchivage)
                .HasConstraintName("Messages_Archivages0_FK");

            entity.HasOne(d => d.IdBroadcastNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdBroadcast)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Messages_BroadcastGroup_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
