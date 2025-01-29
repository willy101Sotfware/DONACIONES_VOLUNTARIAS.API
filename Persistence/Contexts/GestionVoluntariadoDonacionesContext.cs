using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Persistence.Contexts
{
    public partial class GestionVoluntariadoDonacionesContext : DbContext, IGestionVoluntariadoDonacionesContext
    {
        protected GestionVoluntariadoDonacionesContext()
        {
        }

        public GestionVoluntariadoDonacionesContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Donor> Donors { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventParticipation> EventParticipations { get; set; }
        public virtual DbSet<Impact> Impacts { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.HasKey(e => e.DonationId).HasName("PK__Donation__C5082EDB489D2EF9");
                entity.Property(e => e.DonationId).HasColumnName("DonationID");
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.DonationDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.DonorId).HasColumnName("DonorID");
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.HasOne(d => d.Donor).WithMany(p => p.Donations)
                    .HasForeignKey(d => d.DonorId)
                    .HasConstraintName("FK__Donations__Donor__5EBF139D");

                entity.HasOne(d => d.Organization).WithMany(p => p.Donations)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Donations__Organ__5FB337D6");
            });

            modelBuilder.Entity<Donor>(entity =>
            {
                entity.HasKey(e => e.DonorId).HasName("PK__Donors__052E3F98D3A4AE36");
                entity.Property(e => e.DonorId).HasColumnName("DonorID");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.PreferredCauses).HasMaxLength(256);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.Donors)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Donors__UserID__571DF1D5");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId).HasName("PK__Events__7944C8707FC86B06");
                entity.Property(e => e.EventId).HasColumnName("EventID");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.EventDate).HasColumnType("datetime");
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Organization).WithMany(p => p.Events)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Events__Organiza__5AEE82B9");
            });

            modelBuilder.Entity<EventParticipation>(entity =>
            {
                entity.HasKey(e => e.ParticipationId).HasName("PK__EventPar__4EA27080D6087B6F");
                entity.Property(e => e.ParticipationId).HasColumnName("ParticipationID");
                entity.Property(e => e.EventId).HasColumnName("EventID");
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.Property(e => e.ParticipationDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");

                entity.HasOne(d => d.Event).WithMany(p => p.EventParticipations)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__EventPart__Event__6383C8BA");

                entity.HasOne(d => d.Volunteer).WithMany(p => p.EventParticipations)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("FK__EventPart__Volun__6477ECF3");
            });

            modelBuilder.Entity<Impact>(entity =>
            {
                entity.HasKey(e => e.ImpactId).HasName("PK__Impact__2297C5DD6D4EA251");
                entity.ToTable("Impact");
                entity.Property(e => e.ImpactId).HasColumnName("ImpactID");
                entity.Property(e => e.EventId).HasColumnName("EventID");
                entity.Property(e => e.MeasurementDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.MetricName).HasMaxLength(100);
                entity.Property(e => e.MetricValue).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.HasOne(d => d.Event).WithMany(p => p.Impacts)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__Impact__EventID__693CA210");

                entity.HasOne(d => d.Organization).WithMany(p => p.Impacts)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Impact__Organiza__68487DD7");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B72455F617F");
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
                entity.Property(e => e.ContactEmail).HasMaxLength(100);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Website).HasMaxLength(100);

                entity.HasOne(d => d.User).WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Organizat__UserI__4F7CD00D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC1E15B599");
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E4B78389B2").IsUnique();
                entity.HasIndex(e => e.Email, "UQ__Users__A9D105346076606E").IsUnique();
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasMaxLength(256);
                entity.Property(e => e.Role).HasMaxLength(20);
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.VolunteerId).HasName("PK__Voluntee__716F6FCC81664D64");
                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");
                entity.Property(e => e.Availability).HasMaxLength(100);
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Interests).HasMaxLength(256);
                entity.Property(e => e.Skills).HasMaxLength(256);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.Volunteers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Volunteer__UserI__534D60F1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
