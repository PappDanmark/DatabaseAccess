using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class SensorTypeDataAccessTests
{
    private List<SensorType> mockData;
    private Mock<DbSet<SensorType>> mockSensorTypeDbSet;
    private Mock<PappDbContext> mockContext;
    private SensorTypeDataAccess sut;

    public SensorTypeDataAccessTests()
    {
        this.mockData = new List<SensorType> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                Model = "m2",
                Manufacturer = 1
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                Model = "m2",
                Manufacturer = 2
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                Model = "m3",
                Manufacturer = 3
            }
        };

        this.mockSensorTypeDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<SensorType>()).Returns(mockSensorTypeDbSet.Object);
        mockContext.Setup(c => c.SensorTypes).Returns(mockSensorTypeDbSet.Object);

        this.sut = new SensorTypeDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        SensorType SensorType = new Mock<SensorType>().Object;

        // Run SUT
        await sut.AddAsync(SensorType);

        // Verify
        this.mockSensorTypeDbSet.Verify(c => c.AddAsync(It.IsAny<SensorType>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorType>?, IList<SensorType>, Task> runner = async (filter, expected) =>
        {
            IList<SensorType> SensorTypeList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, SensorTypeList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], SensorTypeList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<SensorType>(e => e.Model.Equals("m2")), mockData.Where(e => e.Model.Equals("m2")).ToList());
        await runner(new Specification<SensorType>(e => e.Manufacturer.Equals(1)), new List<SensorType> { mockData[0] });
        await runner(new Specification<SensorType>(e => e.Manufacturer.Equals(-3)), new List<SensorType>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorType>, SensorType?, Task> runner = async (filter, expected) =>
        {
            SensorType? SensorType = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(SensorType);
            }
            else
            {
                Assert.IsNotNull(SensorType);
                Assert.AreEqual(expected, SensorType);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<SensorType>(e => e.Id.ToString().Equals("")), null);
        await runner(new Specification<SensorType>(e => e.Id.ToString().Equals("   ")), null);
        await runner(new Specification<SensorType>(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2")), null);
        await runner(new Specification<SensorType>(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")), mockData[1]);
        await runner(new Specification<SensorType>(e => e.Manufacturer.Equals(-1)), null);
        await runner(new Specification<SensorType>(e => e.Manufacturer.Equals(3)), mockData[2]);
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
