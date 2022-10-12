using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class SensorInstallDataAccessTests
{
    private List<SensorInstall> mockData;
    private Mock<DbSet<SensorInstall>> mockSensorInstallDbSet;
    private Mock<PappDbContext> mockContext;
    private SensorInstallDataAccess sut;

    public SensorInstallDataAccessTests()
    {
        this.mockData = new List<SensorInstall> {
            new() {
                Id = 1,
                Booth = new("dcd6a274-b7fb-41aa-b099-020296b70e5a")
            },
            new() {
                Id = 2,
                Booth = new("dcd6a274-b7fb-41aa-b099-020296b70e5a")
            },
            new() {
                Id = 3,
                Booth = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9")
            }
        };

        this.mockSensorInstallDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<SensorInstall>()).Returns(mockSensorInstallDbSet.Object);
        mockContext.Setup(c => c.SensorInstalls).Returns(mockSensorInstallDbSet.Object);

        this.sut = new SensorInstallDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        SensorInstall SensorInstall = new Mock<SensorInstall>().Object;

        // Run SUT
        await sut.AddAsync(SensorInstall);

        // Verify
        this.mockSensorInstallDbSet.Verify(c => c.AddAsync(It.IsAny<SensorInstall>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorInstall>?, IList<SensorInstall>, Task> runner = async (filter, expected) =>
        {
            IList<SensorInstall> SensorInstallList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, SensorInstallList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], SensorInstallList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<SensorInstall>(e => e.Booth.ToString().Equals("dcd6a274-b7fb-41aa-b099-020296b70e5a")), mockData.Where(e => e.Booth.ToString().Equals("dcd6a274-b7fb-41aa-b099-020296b70e5a")).ToList());
        await runner(new Specification<SensorInstall>(e => e.Booth.ToString().Equals("cab12fe3-a366-4602-bafa-8a92a9cc53f9")), new List<SensorInstall> { mockData[2] });
        await runner(new Specification<SensorInstall>(e => e.Booth.ToString().Equals("")), new List<SensorInstall>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<SensorInstall>, SensorInstall?, Task> runner = async (filter, expected) =>
        {
            SensorInstall? SensorInstall = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(SensorInstall);
            }
            else
            {
                Assert.IsNotNull(SensorInstall);
                Assert.AreEqual(expected, SensorInstall);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<SensorInstall>(e => e.Id.Equals(-3)), null);
        await runner(new Specification<SensorInstall>(e => e.Id.Equals(0)), null);
        await runner(new Specification<SensorInstall>(e => e.Id.Equals(104)), null);
        await runner(new Specification<SensorInstall>(e => e.Id.Equals(2)), mockData[1]);
        await runner(new Specification<SensorInstall>(e => e.Booth.ToString().Equals("  ")), null);
        await runner(new Specification<SensorInstall>(e => e.Booth.ToString().Equals("cab12fe3-a366-4602-bafa-8a92a9cc53f9")), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
