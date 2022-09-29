using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class SensorDataAccessTests
{
    private List<Sensor> mockData;
    private Mock<DbSet<Sensor>> mockSensorDbSet;
    private Mock<PappDbContext> mockContext;
    private SensorDataAccess sut;

    public SensorDataAccessTests()
    {
        this.mockData = new List<Sensor> {
            new() {
                Id = "sensor#1",
                Type = new("029d6427-adf2-4746-a33f-cfc60a51e4e2")
            },
            new() {
                Id = "sensor#2",
                Type = new("029d6427-adf2-4746-a33f-cfc60a51e4e2")
            },
            new() {
                Id = "sensor#3",
                Type = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9")
            }
        };

        this.mockSensorDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Sensor>()).Returns(mockSensorDbSet.Object);
        mockContext.Setup(c => c.Sensors).Returns(mockSensorDbSet.Object);

        this.sut = new SensorDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Sensor Sensor = new Mock<Sensor>().Object;

        // Run SUT
        await sut.AddAsync(Sensor);

        // Verify
        this.mockSensorDbSet.Verify(c => c.AddAsync(It.IsAny<Sensor>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Sensor>?, IList<Sensor>, Task> runner = async (filter, expected) =>
        {
            IList<Sensor> SensorList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, SensorList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], SensorList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Sensor>(e => e.Type.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")), mockData.Where(e => e.Type.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")).ToList());
        await runner(new Specification<Sensor>(e => e.Id.Equals("sensor#1")), new List<Sensor> { mockData[0] });
        await runner(new Specification<Sensor>(e => e.Id.Equals("")), new List<Sensor>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Sensor>, Sensor?, Task> runner = async (filter, expected) =>
        {
            Sensor? Sensor = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Sensor);
            }
            else
            {
                Assert.IsNotNull(Sensor);
                Assert.AreEqual(expected, Sensor);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<Sensor>(e => e.Id.Equals("")), null);
        await runner(new Specification<Sensor>(e => e.Id.Equals("   ")), null);
        await runner(new Specification<Sensor>(e => e.Id.Equals("sensor#10")), null);
        await runner(new Specification<Sensor>(e => e.Id.Equals("sensor#2")), mockData[1]);
        await runner(new Specification<Sensor>(e => e.Type.Equals("")), null);
        await runner(new Specification<Sensor>(e => e.Type.ToString().Equals("cab12fe3-a366-4602-bafa-8a92a9cc53f9")), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, "sensor#104")]
    [DataRow(true, "sensor#1")]
    public async Task Exists(bool expected, string id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
