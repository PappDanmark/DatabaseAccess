using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using System.Linq.Expressions;

namespace Papp.Persistence.Tests;

[TestClass]
public class ParkingBoothDataAccessTests
{
    private List<ParkingBooth> mockData;
    private Mock<DbSet<ParkingBooth>> mockParkingBoothDbSet;
    private Mock<PappDbContext> mockContext;
    private ParkingBoothDataAccess sut;

    public ParkingBoothDataAccessTests()
    {
        this.mockData = new List<ParkingBooth> {
            new() {
                ParkingBoothId = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                BoothNumber = 2
            },
            new() {
                ParkingBoothId = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                BoothNumber = 2
            },
            new() {
                ParkingBoothId = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                BoothNumber = 3
            }
        };

        this.mockParkingBoothDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ParkingBooth>()).Returns(mockParkingBoothDbSet.Object);
        mockContext.Setup(c => c.ParkingBooths).Returns(mockParkingBoothDbSet.Object);

        this.sut = new ParkingBoothDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        ParkingBooth ParkingBooth = new Mock<ParkingBooth>().Object;

        // Run SUT
        await sut.AddAsync(ParkingBooth);

        // Verify
        this.mockParkingBoothDbSet.Verify(c => c.AddAsync(It.IsAny<ParkingBooth>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<Expression<Func<ParkingBooth, bool>>?, IList<ParkingBooth>, Task> runner = async (filter, expected) =>
        {
            IList<ParkingBooth> ParkingBoothList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ParkingBoothList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ParkingBoothList[i]);
            }
        };

        // Actual tests
        // Takes in the optional lamba exp. based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(e => e.BoothNumber.Equals(2), mockData.Where(e => e.BoothNumber.Equals(2)).ToList());
        await runner(e => e.BoothNumber.Equals(3), new List<ParkingBooth> { mockData[2] });
        await runner(e => e.BoothNumber.Equals(-3), new List<ParkingBooth>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<Expression<Func<ParkingBooth, bool>>, ParkingBooth?, Task> runner = async (filter, expected) =>
        {
            ParkingBooth? ParkingBooth = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(ParkingBooth);
            }
            else
            {
                Assert.IsNotNull(ParkingBooth);
                Assert.AreEqual(expected, ParkingBooth);
            }
        };

        // Actual tests
        // Takes in the lamba exp. based on which to filter and the expected
        await runner(e => e.ParkingBoothId.ToString().Equals(""), null);
        await runner(e => e.ParkingBoothId.ToString().Equals("   "), null);
        await runner(e => e.ParkingBoothId.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2"), null);
        await runner(e => e.ParkingBoothId.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2"), mockData[1]);
        await runner(e => e.BoothNumber.Equals(-1), null);
        await runner(e => e.BoothNumber.Equals(3), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, "129d6427-adf2-4746-a33f-cfc60a51e4e2")]
    [DataRow(true, "029d6427-adf2-4746-a33f-cfc60a51e4e2")]
    public async Task Exists(bool expected, string id)
    {
        bool exists = await sut.Exists(new Guid(id));
        Assert.AreEqual(expected, exists);
    }
}
