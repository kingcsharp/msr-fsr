using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Resources.EntityFramework
{
    public class AnswerContext : DbContext
    {
        private const string ConnectionString_ = "server=bang.msr-fsr.com;Initial Catalog=Answer3_Dev;User Id=msrfsr;Password=snRvf2rFVG7rGAVE;";
        public DbSet<User> User { get; set; }

        public AnswerContext() : base()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).UseSqlServer(ConnectionString_);
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        HandleTrackableEntity(entry, now);
                        break;
                        //case EntityState.Deleted:
                        //    HandleDeletedEntry(entry);
                        //    HandleTrackableEntity(entry, now);
                        //    break;
                }
            }

            int result = base.SaveChanges();

            return result;

        }

        private void HandleTrackableEntity(EntityEntry entry, DateTime now)
        {
            TrackableEntity trackable;
            if ((trackable = entry.Entity as TrackableEntity) != null)
            {
                int? answerUserId = GetCurrentUserId();
                if (entry.State == EntityState.Added)
                {
                    trackable.CreatedOn = now;
                    trackable.CreatedBy = answerUserId;
                }
                trackable.LastUpdatedOn = now;
                trackable.LastUpdatedBy = answerUserId;
            }
        }

        private int? GetCurrentUserId()
        {
            //TODO USE A IOC.
            int? currentUserId = null;
            //if (IocContainer.GetCurrentUserId == null)
            //    return null;

            //int? currentUserId = IocContainer.GetCurrentUserId();

            if (currentUserId == 0)
                currentUserId = null;

            return currentUserId;
        }

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            //builder.AddFilter((category, level) =>
            //category == DbLoggerCategory.Database.Command.Name
            //&& level == LogLevel.Information)
            //.AddConsole();
        });


    }
}
