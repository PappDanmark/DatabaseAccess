using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ParkingAreaDataAccessTests
{
    private List<ParkingArea> mockData;
    private Mock<DbSet<ParkingArea>> mockParkingAreaDbSet;
    private Mock<PappDbContext> mockContext;
    private ParkingAreaDataAccess sut;

    public ParkingAreaDataAccessTests()
    {
        this.mockData = new List<ParkingArea> {
            new() {
                Id = 1,
                PappId = "pappId#1",
                ZipCodeId = 8700,
                Name = "space1"
            },
            new() {
                Id = 2,
                PappId = "pappId#2",
                ZipCodeId = 8700,
                Name = "space2"
            },
            new() {
                Id = 3,
                PappId = "pappId#3",
                ZipCodeId = 8700,
                Name = "space3"
            }
        };

        this.mockParkingAreaDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ParkingArea>()).Returns(mockParkingAreaDbSet.Object);
        mockContext.Setup(c => c.ParkingAreas).Returns(mockParkingAreaDbSet.Object);

        this.sut = new ParkingAreaDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync(id);
        Assert.AreEqual(expected, exists);
    }
}
