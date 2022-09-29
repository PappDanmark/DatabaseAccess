using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class ParkingAreaDataAccessTests
{
    private List<ParkingArea> mockData;
    private Mock<DbSet<ParkingArea>> mockParkingAreaDbSet;
    private Mock<PappDbContext> mockContext;
    private ParkingAreaDataAccess sut;

    public ParkingAreaDataAccessTests()
    {
        this.mockData = new List<ParkingArea> {
            new() {
                Id = 1,
                PappId = "pappId#1",
                ZipCodeId = 8700,
                Name = "space1"
            },
            new() {
                Id = 2,
                PappId = "pappId#2",
                ZipCodeId = 8700,
                Name = "space2"
            },
            new() {
                Id = 3,
                PappId = "pappId#3",
                ZipCodeId = 8700,
                Name = "space3"
            }
        };

        this.mockParkingAreaDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ParkingArea>()).Returns(mockParkingAreaDbSet.Object);
        mockContext.Setup(c => c.ParkingAreas).Returns(mockParkingAreaDbSet.Object);

        this.sut = new ParkingAreaDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        ParkingArea ParkingArea = new Mock<ParkingArea>().Object;

        // Run SUT
        await sut.AddAsync(ParkingArea);

        // Verify
        this.mockParkingAreaDbSet.Verify(c => c.AddAsync(It.IsAny<ParkingArea>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ParkingArea>?, IList<ParkingArea>, Task> runner = async (filter, expected) =>
        {
            IList<ParkingArea> ParkingAreaList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ParkingAreaList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ParkingAreaList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<ParkingArea>(e => e.ZipCodeId.Equals(8700)), mockData.Where(e => e.ZipCodeId.Equals(8700)).ToList());
        await runner(new Specification<ParkingArea>(e => e.Id.Equals(1)), new List<ParkingArea> { mockData[0] });
        await runner(new Specification<ParkingArea>(e => e.Id.Equals(-3)), new List<ParkingArea>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ParkingArea>, ParkingArea?, Task> runner = async (filter, expected) =>
        {
            ParkingArea? ParkingArea = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(ParkingArea);
            }
            else
            {
                Assert.IsNotNull(ParkingArea);
                Assert.AreEqual(expected, ParkingArea);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<ParkingArea>(e => e.Id.Equals(-3)), null);
        await runner(new Specification<ParkingArea>(e => e.Id.Equals(2)), mockData[1]);
        await runner(new Specification<ParkingArea>(e => e.PappId.Equals("abcd")), null);
        await runner(new Specification<ParkingArea>(e => e.PappId.Equals("pappId#3")), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
