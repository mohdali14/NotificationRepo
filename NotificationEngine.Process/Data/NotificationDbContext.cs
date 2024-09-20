using Microsoft.EntityFrameworkCore;

namespace NotificationEngine.Process.Data
{
    public class NotificationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet for the Policy entity.
        /// This property is used to query and save instances of the <see cref="Policy"/> entity.
        /// </summary>
        public DbSet<Policy> Policies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.ToTable("Policy", "dbo");

                entity.HasIndex(e => e.EpicUniqPolicy, "IX_Policy");

                entity.HasIndex(e => e.ExpirationDate, "IX_Policy_ExpirationDate");

                entity.Property(e => e.Description).HasMaxLength(125);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.Number).HasMaxLength(25);

            });
        }
    }
}
