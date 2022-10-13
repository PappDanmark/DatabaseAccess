using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;
using System.Linq.Expressions;

namespace Papp.Persistence.Tests;

[TestClass]
public class GenericDataAccessTests
{
    private IList<Booth> mockData;
    private Mock<DbSet<Booth>> mockDbSet;
    private Mock<PappDbContext> mockContext;

    private IGenericDataAccess<Booth> sut;

    public GenericDataAccessTests()
    {
        this.mockData = new List<Booth> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                BoothNumber = 1,
                MuncipalityId = "m2"
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                BoothNumber = 2,
                MuncipalityId = "m2"
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                BoothNumber = 3,
                MuncipalityId = "m3"
            }
        };

        this.mockDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        
        mockContext.Setup(c => c.Set<Booth>()).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.Booths).Returns(mockDbSet.Object);

        this.sut = new GenericDataAccess<Booth>(mockContext.Object);
    }

    // All tests related to Read methods:
    #region Read

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public void GetFirstOrDefault()
    {
        // Implementation of the test
        Action<Expression<Func<Booth, bool>>, Booth?> runner = (filter, expected) =>
        {
            // Run SUT
            Booth? Booth = sut.GetFirstOrDefault(filter);
            if (expected == null)
            {
                Assert.IsNull(Booth);
            }
            else
            {
                Assert.IsNotNull(Booth);
                Assert.AreEqual(expected, Booth);
            }
        };

        // Run tests
        // Takes in the lamba exp. based on which to filter and the expected
        runner(e => e.Id.ToString().Equals(""), null);
        runner(e => e.Id.ToString().Equals("   "), null);
        runner(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2"), null);
        runner(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2"), mockData[1]);
        runner(e => e.BoothNumber.Equals(-1), null);
        runner(e => e.BoothNumber.Equals(3), mockData[2]);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<Expression<Func<Booth, bool>>, Booth?, Task> runner = async (filter, expected) =>
        {
            Booth? booth = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(booth);
            }
            else
            {
                Assert.IsNotNull(booth);
                Assert.AreEqual(expected, booth);
            }
        };

        // Actual tests
        // Takes in the lamba exp. based on which to filter and the expected
        await runner(e => e.Id.ToString().Equals(""), null);
        await runner(e => e.Id.ToString().Equals("   "), null);
        await runner(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2"), null);
        await runner(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2"), mockData[1]);
        await runner(e => e.BoothNumber.Equals(-1), null);
        await runner(e => e.BoothNumber.Equals(3), mockData[2]);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public void GetAllAsEnumerable()
    {
        // Implementation of the test
        Action<Expression<Func<Booth, bool>>?, IList<Booth>> runner = (filter, expected) =>
        {
            var list = sut.GetAllAsEnumerable(filter);

            Assert.AreEqual(expected.Count, list.Count());
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], list.ElementAt(i));
            }
        };

        // Actual tests
        // Takes in the optional lamba exp. based on which to filter and the expected list of entities
        runner(null, mockData);
        runner(e => e.MuncipalityId.Equals("m2"), mockData.Where(e => e.MuncipalityId.Equals("m2")).ToList());
        runner(e => e.BoothNumber.Equals(1), new List<Booth> { mockData[0] });
        runner(e => e.BoothNumber.Equals(-3), new List<Booth>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public void GetAllAsCollection()
    {
        // Implementation of the test
        Action<Expression<Func<Booth, bool>>?, IList<Booth>> runner = (filter, expected) =>
        {
            var list = sut.GetAllAsCollection(filter);

            Assert.AreEqual(expected.Count, list.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], list.ElementAt(i));
            }
        };

        // Actual tests
        // Takes in the optional lamba exp. based on which to filter and the expected list of entities
        runner(null, mockData);
        runner(e => e.MuncipalityId.Equals("m2"), mockData.Where(e => e.MuncipalityId.Equals("m2")).ToList());
        runner(e => e.BoothNumber.Equals(1), new List<Booth> { mockData[0] });
        runner(e => e.BoothNumber.Equals(-3), new List<Booth>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsCollectionAsync()
    {
        // Implementation of the test
        Func<Expression<Func<Booth, bool>>?, IList<Booth>, Task> runner = async (filter, expected) =>
        {
            var list = await sut.GetAllAsCollectionAsync(filter);

            Assert.AreEqual(expected.Count, list.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], list.ElementAt(i));
            }
        };

        // Actual tests
        // Takes in the optional lamba exp. based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(e => e.MuncipalityId.Equals("m2"), mockData.Where(e => e.MuncipalityId.Equals("m2")).ToList());
        await runner(e => e.BoothNumber.Equals(1), new List<Booth> { mockData[0] });
        await runner(e => e.BoothNumber.Equals(-3), new List<Booth>());
    }

    #endregion

    // All tests related to Create methods:
    #region Add

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public void Add()
    {
        // Setup
        var Booth = new Mock<Booth>().Object;

        // Run SUT
        sut.Add(Booth);

        // Verify
        this.mockDbSet.Verify(c => c.Add(It.IsAny<Booth>()), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        // Setup
        var Booth = new Mock<Booth>().Object;

        // Run SUT
        await sut.AddAsync(Booth);

        // Verify
        this.mockDbSet.Verify(c => c.AddAsync(It.IsAny<Booth>(), default), Times.Once);
    }

    #endregion
}
