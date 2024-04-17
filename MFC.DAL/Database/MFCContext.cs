using Microsoft.EntityFrameworkCore;
using MFC.CORE.Models;

namespace MFC.DAL.Database
{
    public class MFCContext : DbContext
    {
        public MFCContext(DbContextOptions<MFCContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<LiveSession> LiveSessions { get; set; }
        public DbSet<Meditation> Meditations { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<WellnessPlan> WellnessPlans { get; set; }
        public DbSet<DailyAffirmation> DailyAffirmations { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
