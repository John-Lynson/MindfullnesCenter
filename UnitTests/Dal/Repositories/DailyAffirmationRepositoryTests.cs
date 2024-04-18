using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MFC.CORE.Models;
using MFC.DAL.Database;
using MFC.DAL.Repositories;

public class DailyAffirmationRepositoryTests
{
    private MFCContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MFCContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase" + System.Guid.NewGuid())
            .Options;

        var context = new MFCContext(options);
        return context;
    }

    [Fact]
    public async Task GetAffirmationAsync_Returns_Affirmation()
    {
        using (var context = CreateContext())
        {
            var repository = new DailyAffirmationRepository(context);

            context.DailyAffirmations.Add(new DailyAffirmation { Id = 1, Message = "Be positive" });
            context.DailyAffirmations.Add(new DailyAffirmation { Id = 2, Message = "Keep going" });
            await context.SaveChangesAsync();

            var result = await repository.GetAffirmationAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Be positive", result.Message);
        }
    }

    [Fact]
    public async Task GetAllAffirmationsAsync_Returns_All_Affirmations()
    {
        using (var context = CreateContext())
        {
            var repository = new DailyAffirmationRepository(context);

            context.DailyAffirmations.Add(new DailyAffirmation { Id = 1, Message = "Test 1" });
            context.DailyAffirmations.Add(new DailyAffirmation { Id = 2, Message = "Test 2" });
            await context.SaveChangesAsync();

            var affirmations = await repository.GetAllAffirmationsAsync();

            Assert.NotNull(affirmations);
            Assert.Equal(2, affirmations.Count());
        }
    }

    [Fact]
    public async Task AddAffirmationAsync_Adds_Affirmation()
    {
        using (var context = CreateContext())
        {
            var repository = new DailyAffirmationRepository(context);

            var affirmation = new DailyAffirmation { Id = 3, Message = "Stay strong" };

            await repository.AddAffirmationAsync(affirmation);
            await context.SaveChangesAsync();

            var addedAffirmation = await context.DailyAffirmations.FindAsync(3);

            Assert.NotNull(addedAffirmation);
            Assert.Equal("Stay strong", addedAffirmation.Message);
        }
    }
}
