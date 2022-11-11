using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.Context;
using Papp.Persistence.Tests.Data;
using Xunit;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

public class BoothDataAccessTests
{
    private Mock<DbSet<Booth>> mockDbSet;
    private Mock<PappDbContext> mockContext;
    private BoothDataAccess sut;

    public BoothDataAccessTests()
    {
        this.mockDbSet = PappDbDataSet.Booths.AsQueryable().BuildMockDbSet();
        this.mockContext = new();
        this.mockContext.Setup(c => c.Set<Booth>()).Returns(mockDbSet.Object);
        this.mockContext.Setup(c => c.Booths).Returns(mockDbSet.Object);
        this.sut = new BoothDataAccess(mockContext.Object);
    }

    // All tests related to Read methods:
    #region Read

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.FirstOrDefault), MemberType = typeof(BoothDataAccessDataSource))]
    public void FirstOrDefault(
        Booth? expected,
        Expression<Func<Booth, bool>>? predicate,
        Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy,
        Func<IQueryable<Booth>, IIncludableQueryable<Booth, object>>? include,
        bool tracking
    )
    {
        // Run SUT
        Booth? entity = sut.FirstOrDefault(predicate, orderBy, include, tracking);

        // Verify
        if (expected == null)
        {
            Assert.Null(entity);
        }
        else
        {
            Assert.NotNull(entity);
            Assert.Equal(expected, entity);
        }
    }

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.FirstOrDefault), MemberType = typeof(BoothDataAccessDataSource))]
    public async Task FirstOrDefaultAsync(
        Booth? expected,
        Expression<Func<Booth, bool>>? predicate,
        Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy,
        Func<IQueryable<Booth>, IIncludableQueryable<Booth, object>>? include,
        bool tracking
    )
    {
        // Run SUT
        Booth? entity = await sut.FirstOrDefaultAsync(predicate, orderBy, include, tracking);

        // Verify
        if (expected == null)
        {
            Assert.Null(entity);
        }
        else
        {
            Assert.NotNull(entity);
            Assert.Equal(expected, entity);
        }
    }

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.GetAllAsEnumerable), MemberType = typeof(BoothDataAccessDataSource))]
    public void GetAllAsEnumerable(
        IEnumerable<Booth> expected,
        Expression<Func<Booth, bool>>? predicate,
        Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy,
        Func<IQueryable<Booth>, IIncludableQueryable<Booth, object>>? include,
        bool tracking = false
    )
    {
        // Run SUT
        var list = sut.GetAllAsEnumerable(predicate, orderBy, include, tracking);

        // Verify
        Assert.Equal(expected.Count(), list.Count());
        // If the retrieved subset of entities matches the expected number of entities,
        // proceed to compare each one, making sure the retrieved list is correct.
        for (int i = 0; i < expected.Count(); i++)
        {
            Assert.Equal(expected.ElementAt(i), list.ElementAt(i));
        }
    }

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.GetAllAsCollection), MemberType = typeof(BoothDataAccessDataSource))]
    public void GetAllAsCollection(
        ICollection<Booth> expected,
        Expression<Func<Booth, bool>>? predicate,
        Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy,
        Func<IQueryable<Booth>, IIncludableQueryable<Booth, object>>? include,
        bool tracking = false
    )
    {
        // Run SUT
        var list = sut.GetAllAsCollection(predicate, orderBy, include, tracking);

        // Verify
        Assert.Equal(expected.Count, list.Count);
        // If the retrieved subset of entities matches the expected number of entities,
        // proceed to compare each one, making sure the retrieved list is correct.
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected.ElementAt(i), list.ElementAt(i));
        }
    }

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.GetAllAsCollection), MemberType = typeof(BoothDataAccessDataSource))]
    public async Task GetAllAsCollectionAsync(
        ICollection<Booth> expected,
        Expression<Func<Booth, bool>>? predicate,
        Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy,
        Func<IQueryable<Booth>, IIncludableQueryable<Booth, object>>? include,
        bool tracking = false
    )
    {
        // Run SUT
        var list = await sut.GetAllAsCollectionAsync(predicate, orderBy, include, tracking);

        // Verify
        Assert.Equal(expected.Count, list.Count);
        // If the retrieved subset of entities matches the expected number of entities,
        // proceed to compare each one, making sure the retrieved list is correct.
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected.ElementAt(i), list.ElementAt(i));
        }
    }

    #endregion

    // All tests related to Create methods:
    #region Add

    [Fact]
    public async Task AddAsync()
    {
        // Setup
        var booth = new Mock<Booth>().Object;

        // Run SUT
        await sut.AddAsync(booth);

        // Verify
        this.mockDbSet.Verify(c => c.AddAsync(It.IsAny<Booth>(), default), Times.Once);
    }

    [Fact]
    public void AddRange()
    {
        // Setup
        var booths = new Mock<IEnumerable<Booth>>().Object;

        // Run SUT
        sut.AddRange(booths);

        // Verify
        this.mockDbSet.Verify(c => c.AddRange(It.IsAny<IEnumerable<Booth>>()), Times.Once);
    }

    [Fact]
    public async Task AddRangeAsync()
    {
        // Setup
        var booths = new Mock<IEnumerable<Booth>>().Object;

        // Run SUT
        await sut.AddRangeAsync(booths);

        // Verify
        this.mockDbSet.Verify(c => c.AddRangeAsync(It.IsAny<IEnumerable<Booth>>(), default), Times.Once);
    }

    #endregion

    // All tests related to Create methods:
    #region Other

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.Exists), MemberType = typeof(BoothDataAccessDataSource))]
    public void Exists(bool expected, Expression<Func<Booth, bool>>? predicate)
    {
        // Run SUT
        bool actual = sut.Exists(predicate);

        // Verify
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(BoothDataAccessDataSource.Exists), MemberType = typeof(BoothDataAccessDataSource))]
    public async Task ExistsAsync(bool expected, Expression<Func<Booth, bool>>? predicate)
    {
        // Run SUT
        bool actual = await sut.ExistsAsync(predicate);

        // Verify
        Assert.Equal(expected, actual);
    }

    #endregion
}
 