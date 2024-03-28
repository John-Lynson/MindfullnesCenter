using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MFC.DAL
{
    public class MFCContextFactory : IDesignTimeDbContextFactory<MFCContext>
    {
        public MFCContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<MFCContext>();
            var connectionString = configuration.GetConnectionString("MFCDatabase");

            builder.UseSqlServer(connectionString);

            return new MFCContext(builder.Options);
        }
    }
}
