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

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync(id);
        Assert.AreEqual(expected, exists);
    }
}
