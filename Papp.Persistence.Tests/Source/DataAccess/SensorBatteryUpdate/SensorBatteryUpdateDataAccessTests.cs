using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class SensorBatteryUpdateDataAccessTests
{
    private List<SensorBatteryUpdate> mockData;
    private Mock<DbSet<SensorBatteryUpdate>> mockSensorBatteryUpdateDbSet;
    private Mock<PappDbContext> mockContext;
    private SensorBatteryUpdateDataAccess sut;

    public SensorBatteryUpdateDataAccessTests()
    {
        this.mockData = new List<SensorBatteryUpdate> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                Percent = 10.25F,
                SensorId = "sbu1"
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                Percent = 10.25F,
                SensorId = "sbu2"
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                Percent = 8.50F,
                SensorId = "sbu3"
            }
        };

        this.mockSensorBatteryUpdateDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<SensorBatteryUpdate>()).Returns(mockSensorBatteryUpdateDbSet.Object);
        mockContext.Setup(c => c.SensorBatteryUpdates).Returns(mockSensorBatteryUpdateDbSet.Object);

        this.sut = new SensorBatteryUpdateDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        SensorBatteryUpdate SensorBatteryUpdate = new Mock<SensorBatteryUpdate>().Object;

        // Run SUT
        await sut.AddAsync(SensorBatteryUpdate);

        // Verify
        this.mockSensorBatteryUpdateDbSet.Verify(c => c.AddAsync(It.IsAny<SensorBatteryUpdate>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorBatteryUpdate>?, IList<SensorBatteryUpdate>, Task> runner = async (filter, expected) =>
        {
            IList<SensorBatteryUpdate> SensorBatteryUpdateList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, SensorBatteryUpdateList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], SensorBatteryUpdateList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<SensorBatteryUpdate>(e => e.Percent.Equals(10.25F)), mockData.Where(e => e.Percent.Equals(10.25F)).ToList());
        await runner(new Specification<SensorBatteryUpdate>(e => e.SensorId.Equals("sbu3")), new List<SensorBatteryUpdate> { mockData[2] });
        await runner(new Specification<SensorBatteryUpdate>(e => e.SensorId.Equals(" ")), new List<SensorBatteryUpdate>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorBatteryUpdate>, SensorBatteryUpdate?, Task> runner = async (filter, expected) =>
        {
            SensorBatteryUpdate? SensorBatteryUpdate = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(SensorBatteryUpdate);
            }
            else
            {
                Assert.IsNotNull(SensorBatteryUpdate);
                Assert.AreEqual(expected, SensorBatteryUpdate);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<SensorBatteryUpdate>(e => e.Id.ToString().Equals("")), null);
        await runner(new Specification<SensorBatteryUpdate>(e => e.Id.ToString().Equals("   ")), null);
        await runner(new Specification<SensorBatteryUpdate>(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2")), null);
        await runner(new Specification<SensorBatteryUpdate>(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")), mockData[1]);
        await runner(new Specification<SensorBatteryUpdate>(e => e.SensorId.Equals("  ")), null);
        await runner(new Specification<SensorBatteryUpdate>(e => e.Percent.Equals(8.50F)), mockData[2]);
    }
}
