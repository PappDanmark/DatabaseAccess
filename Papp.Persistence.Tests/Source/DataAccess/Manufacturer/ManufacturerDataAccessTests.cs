using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ManufacturerDataAccessTests
{
    private List<Manufacturer> mockData;
    private Mock<DbSet<Manufacturer>> mockManufacturerDbSet;
    private Mock<PappDbContext> mockContext;
    private ManufacturerDataAccess sut;

    public ManufacturerDataAccessTests()
    {
        this.mockData = new List<Manufacturer> {
            new() {
                Id = 1,
                Name = "m1"
            },
            new() {
                Id = 2,
                Name = "m1"
            },
            new() {
                Id = 3,
                Name = "m3"
            }
        };

        this.mockManufacturerDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Manufacturer>()).Returns(mockManufacturerDbSet.Object);
        mockContext.Setup(c => c.Manufacturers).Returns(mockManufacturerDbSet.Object);

        this.sut = new ManufacturerDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Manufacturer Manufacturer = new Mock<Manufacturer>().Object;

        // Run SUT
        await sut.AddAsync(Manufacturer);

        // Verify
        this.mockManufacturerDbSet.Verify(c => c.AddAsync(It.IsAny<Manufacturer>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Manufacturer>?, IList<Manufacturer>, Task> runner = async (filter, expected) =>
        {
            IList<Manufacturer> ManufacturerList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ManufacturerList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ManufacturerList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Manufacturer>(e => e.Name.Equals("m1")), mockData.Where(e => e.Name.Equals("m1")).ToList());
        await runner(new Specification<Manufacturer>(e => e.Name.Equals("m3")), new List<Manufacturer> { mockData[2] });
        await runner(new Specification<Manufacturer>(e => e.Name.Equals("  ")), new List<Manufacturer>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Manufacturer>, Manufacturer?, Task> runner = async (filter, expected) =>
        {
            Manufacturer? Manufacturer = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Manufacturer);
            }
            else
            {
                Assert.IsNotNull(Manufacturer);
                Assert.AreEqual(expected, Manufacturer);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<Manufacturer>(e => e.Id.Equals(-1)), null);
        await runner(new Specification<Manufacturer>(e => e.Id.Equals(2)), mockData[1]);
        await runner(new Specification<Manufacturer>(e => e.Name.Equals("")), null);
        await runner(new Specification<Manufacturer>(e => e.Name.Equals("   ")), null);
        await runner(new Specification<Manufacturer>(e => e.Name.Equals("m3")), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -2)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists((short) id);
        Assert.AreEqual(expected, exists);
    }
}
