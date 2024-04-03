using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MFC.CORE.Models;
using MFC.DAL.Repositories;
using MFC.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class DailyAffirmationRepositoryTests
{
    private readonly Mock<DbSet<DailyAffirmation>> _mockSet;
    private readonly Mock<MFCContext> _mockContext;
    private readonly DailyAffirmationRepository _repository;

    public DailyAffirmationRepositoryTests()
    {
        _mockSet = new Mock<DbSet<DailyAffirmation>>();
        _mockContext = new Mock<MFCContext>();
        _repository = new DailyAffirmationRepository(_mockContext.Object);

        _mockContext.Setup(m => m.DailyAffirmations).Returns(_mockSet.Object);
    }

    [Fact]
    public async Task GetAffirmationAsync_Returns_Affirmation()
    {
        var affirmations = new List<DailyAffirmation>
        {
            new DailyAffirmation { Id = 1, Message = "Be positive" },
            new DailyAffirmation { Id = 2, Message = "Keep going" }
        }.AsQueryable();

        _mockSet.As<IQueryable<DailyAffirmation>>().Setup(m => m.Provider).Returns(affirmations.Provider);
        _mockSet.As<IQueryable<DailyAffirmation>>().Setup(m => m.Expression).Returns(affirmations.Expression);
        _mockSet.As<IQueryable<DailyAffirmation>>().Setup(m => m.ElementType).Returns(affirmations.ElementType);
        _mockSet.As<IQueryable<DailyAffirmation>>().Setup(m => m.GetEnumerator()).Returns(affirmations.GetEnumerator());

        var result = await _repository.GetAffirmationAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Be positive", result.Message);
    }

    [Fact]
    public async Task GetAllAffirmationsAsync_Returns_All_Affirmations()
    {
        // Soortgelijke setup als GetAffirmationAsync, maar test voor het ophalen van alle affirmations
    }

    [Fact]
    public async Task AddAffirmationAsync_Adds_Affirmation()
    {
        var affirmation = new DailyAffirmation { Id = 3, Message = "Stay strong" };

        _mockSet.Setup(m => m.AddAsync(It.IsAny<DailyAffirmation>(), default)).ReturnsAsync((DailyAffirmation affirmation, CancellationToken token) => affirmation);

        await _repository.AddAffirmationAsync(affirmation);

        _mockSet.Verify(m => m.AddAsync(It.Is<DailyAffirmation>(a => a == affirmation), default), Times.Once());
        _mockContext.Verify(m => m.SaveChangesAsync(default(CancellationToken)), Times.Once());
    }
}
