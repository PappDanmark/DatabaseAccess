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
                SensorId = "sbu1"
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                SensorId = "sbu2"
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                SensorId = "sbu3"
            }
        };

        this.mockSensorBatteryUpdateDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<SensorBatteryUpdate>()).Returns(mockSensorBatteryUpdateDbSet.Object);
        mockContext.Setup(c => c.SensorBatteryUpdates).Returns(mockSensorBatteryUpdateDbSet.Object);

        this.sut = new SensorBatteryUpdateDataAccess(mockContext.Object);
    }
}
