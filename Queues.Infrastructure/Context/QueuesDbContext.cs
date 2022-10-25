using Microsoft.EntityFrameworkCore;
using Queues.Domain.Entities;
using Queues.Infrastructure.Context.Extensions;

namespace Queues.Infrastructure.Context
{
    public interface IQueuesDbContext
    {
        public DbSet<T> GetDbSet<T>() where T : BaseEntity;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class QueuesDbContext : DbContext, IQueuesDbContext
    {
        public DbSet<Person> People { get; set; } = null!;
        public DbSet<Queue> Queues { get; set; } = null!;
        public DbSet<PersonQueue> PeopleQueues { get; set; } = null!;

        public QueuesDbContext(DbContextOptions<QueuesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddPeopleMapping();
            modelBuilder.AddQueuesMapping();
            modelBuilder.AddPeopleQueuesMapping();
        }

        public DbSet<T> GetDbSet<T>() where T : BaseEntity => Set<T>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
