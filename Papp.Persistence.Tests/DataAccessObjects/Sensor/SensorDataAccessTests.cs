using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

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
                SensorId = "sensor#1",
                SensorTypeId = new("029d6427-adf2-4746-a33f-cfc60a51e4e2")
            },
            new() {
                SensorId = "sensor#2",
                SensorTypeId = new("029d6427-adf2-4746-a33f-cfc60a51e4e2")
            },
            new() {
                SensorId = "sensor#3",
                SensorTypeId = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9")
            }
        };

        this.mockSensorDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Sensor>()).Returns(mockSensorDbSet.Object);
        mockContext.Setup(c => c.Sensors).Returns(mockSensorDbSet.Object);

        this.sut = new SensorDataAccess(mockContext.Object);
    }
}
