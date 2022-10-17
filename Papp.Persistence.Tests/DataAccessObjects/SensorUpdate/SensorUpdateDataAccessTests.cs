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
}
