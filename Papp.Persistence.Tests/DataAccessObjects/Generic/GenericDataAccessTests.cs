using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;
using System.Linq.Expressions;
using Xunit;
using Papp.Persistence.Tests.Data;

namespace Papp.Persistence.Tests;

public class GenericDataAccessTests
{
    private Mock<DbSet<Booth>> mockDbSet;
    private Mock<PappDbContext> mockContext;
    private IGenericDataAccess<Booth> sut;

    public GenericDataAccessTests()
    {
        this.mockDbSet = PappDbDataSet.Booths.AsQueryable().BuildMockDbSet();

        this.mockContext = new();

        mockContext.Setup(c => c.Set<Booth>()).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.Booths).Returns(mockDbSet.Object);

        this.sut = new GenericDataAccess<Booth>(mockContext.Object);
    }

    // All tests related to Read methods:
    #region Read

    [Theory]
    [MemberData(nameof(GenericDataAccessDataSource.GetFirstOrDefault), MemberType = typeof(GenericDataAccessDataSource))]
    public void GetFirstOrDefault(Booth? expected, Expression<Func<Booth, bool>> filter, Func<IQueryable<Booth>?, IOrderedQueryable<Booth>>? orderBy, bool tracking)
    {
        // Run SUT
        Booth? entity = sut.GetFirstOrDefault(filter, orderBy, null, tracking);

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
    [MemberData(nameof(GenericDataAccessDataSource.GetFirstOrDefaultAsync), MemberType = typeof(GenericDataAccessDataSource))]
    public async Task GetFirstOrDefaultAsync(Booth? expected, Expression<Func<Booth, bool>> filter, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy, bool tracking)
    {
        // Run SUT
        Booth? entity = await sut.GetFirstOrDefaultAsync(filter, orderBy, null, tracking);

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
    [MemberData(nameof(GenericDataAccessDataSource.GetAllAsEnumerable), MemberType = typeof(GenericDataAccessDataSource))]
    public void GetAllAsEnumerable(ICollection<Booth> expected, Expression<Func<Booth, bool>>? filter, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy, bool tracking)
    {
        // Run SUT
        var list = sut.GetAllAsEnumerable(filter);

        // Verify
        Assert.Equal(expected.Count, list.Count());
        // If the retrieved subset of entities matches the expected number of entities,
        // proceed to compare each one, making sure the retrieved list is correct.
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected.ElementAt(i), list.ElementAt(i));
        }
    }

    [Theory]
    [MemberData(nameof(GenericDataAccessDataSource.GetAllAsCollection), MemberType = typeof(GenericDataAccessDataSource))]
    public void GetAllAsCollection(ICollection<Booth> expected, Expression<Func<Booth, bool>>? filter, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy, bool tracking)
    {
        // Run SUT
        var list = sut.GetAllAsCollection(filter);

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
    [MemberData(nameof(GenericDataAccessDataSource.GetAllAsCollectionAsync), MemberType = typeof(GenericDataAccessDataSource))]
    public async Task GetAllAsCollectionAsync(ICollection<Booth> expected, Expression<Func<Booth, bool>>? filter, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>? orderBy, bool tracking)
    {
        // Run SUT
        var list = await sut.GetAllAsCollectionAsync(filter);

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
}
