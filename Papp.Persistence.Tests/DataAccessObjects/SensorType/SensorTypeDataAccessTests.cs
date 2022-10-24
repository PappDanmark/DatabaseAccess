using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

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
}
