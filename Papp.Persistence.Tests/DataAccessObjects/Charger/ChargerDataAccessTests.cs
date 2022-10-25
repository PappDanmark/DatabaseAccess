using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ChargerDataAccessTests
{
    private List<Charger> mockData;
    private Mock<DbSet<Charger>> mockChargerDbSet;
    private Mock<PappDbContext> mockContext;
    private ChargerDataAccess sut;

    public ChargerDataAccessTests()
    {
        this.mockData = new List<Charger> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                OperatorId = "#1",
                ChargerType = 2
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                OperatorId = "#2",
                ChargerType = 3
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                OperatorId = "#3",
                ChargerType = 2
            }
        };

        this.mockChargerDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Charger>()).Returns(mockChargerDbSet.Object);
        mockContext.Setup(c => c.Chargers).Returns(mockChargerDbSet.Object);

        this.sut = new ChargerDataAccess(mockContext.Object);
    }
}
