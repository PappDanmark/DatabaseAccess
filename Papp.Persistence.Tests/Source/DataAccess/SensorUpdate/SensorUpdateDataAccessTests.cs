using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class SensorUpdateDataAccessTests
{
    private List<SensorUpdate> mockData;
    private Mock<DbSet<SensorUpdate>> mockSensorUpdateDbSet;
    private Mock<PappDbContext> mockContext;
    private SensorUpdateDataAccess sut;

    public SensorUpdateDataAccessTests()
    {
        this.mockData = new List<SensorUpdate> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                SensorId = "sensor#1",
                Occupied = true
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                SensorId = "sensor#2",
                Occupied = false
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                SensorId = "sensor#3",
                Occupied = true
            }
        };

        this.mockSensorUpdateDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<SensorUpdate>()).Returns(mockSensorUpdateDbSet.Object);
        mockContext.Setup(c => c.SensorUpdates).Returns(mockSensorUpdateDbSet.Object);

        this.sut = new SensorUpdateDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        SensorUpdate SensorUpdate = new Mock<SensorUpdate>().Object;

        // Run SUT
        await sut.AddAsync(SensorUpdate);

        // Verify
        this.mockSensorUpdateDbSet.Verify(c => c.AddAsync(It.IsAny<SensorUpdate>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorUpdate>?, IList<SensorUpdate>, Task> runner = async (filter, expected) =>
        {
            IList<SensorUpdate> SensorUpdateList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, SensorUpdateList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], SensorUpdateList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<SensorUpdate>(e => e.Occupied.Equals(true)), mockData.Where(e => e.Occupied.Equals(true)).ToList());
        await runner(new Specification<SensorUpdate>(e => e.SensorId.Equals("sensor#1")), new List<SensorUpdate> { mockData[0] });
        await runner(new Specification<SensorUpdate>(e => e.SensorId.Equals("")), new List<SensorUpdate>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorUpdate>, SensorUpdate?, Task> runner = async (filter, expected) =>
        {
            SensorUpdate? SensorUpdate = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(SensorUpdate);
            }
            else
            {
                Assert.IsNotNull(SensorUpdate);
                Assert.AreEqual(expected, SensorUpdate);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<SensorUpdate>(e => e.Id.ToString().Equals("")), null);
        await runner(new Specification<SensorUpdate>(e => e.Id.ToString().Equals("   ")), null);
        await runner(new Specification<SensorUpdate>(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2")), null);
        await runner(new Specification<SensorUpdate>(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")), mockData[1]);
        await runner(new Specification<SensorUpdate>(e => e.SensorId.Equals("   ")), null);
        await runner(new Specification<SensorUpdate>(e => e.SensorId.Equals("sensor#3")), mockData[2]);
    }
}
