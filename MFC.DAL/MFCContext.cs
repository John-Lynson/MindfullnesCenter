using Microsoft.EntityFrameworkCore;
using MFC.CORE.Models; // Verwijs naar je modellen

namespace MFC.DAL
{
    public class MFCContext : DbContext
    {
        public MFCContext(DbContextOptions<MFCContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        // Definieer andere DbSet<Entiteit> voor je modellen
    }
}
