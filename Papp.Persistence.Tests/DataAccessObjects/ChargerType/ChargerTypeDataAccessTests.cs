using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ChargerTypeDataAccessTests
{
    private List<ChargerType> mockData;
    private Mock<DbSet<ChargerType>> mockChargerTypeDbSet;
    private Mock<PappDbContext> mockContext;
    private ChargerTypeDataAccess sut;

    public ChargerTypeDataAccessTests()
    {
        this.mockData = new List<ChargerType> {
            new() {
                Id = 1,
                Operator = 2,
                Kilowatt = 25,
                Dc = false,
                Name = "ChT#1"
            },
            new() {
                Id = 2,
                Operator = 2,
                Kilowatt = 25,
                Dc = true,
                Name = "ChT#2"
            },
            new() {
                Id = 3,
                Operator = 3,
                Kilowatt = 20,
                Dc = true,
                Name = "ChT#3"
            }
        };

        this.mockChargerTypeDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ChargerType>()).Returns(mockChargerTypeDbSet.Object);
        mockContext.Setup(c => c.ChargerTypes).Returns(mockChargerTypeDbSet.Object);

        this.sut = new ChargerTypeDataAccess(mockContext.Object);
    }
}
