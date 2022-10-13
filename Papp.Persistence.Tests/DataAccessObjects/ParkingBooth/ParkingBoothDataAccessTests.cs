using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ParkingBoothDataAccessTests
{
    private List<ParkingBooth> mockData;
    private Mock<DbSet<ParkingBooth>> mockParkingBoothDbSet;
    private Mock<PappDbContext> mockContext;
    private ParkingBoothDataAccess sut;

    public ParkingBoothDataAccessTests()
    {
        this.mockData = new List<ParkingBooth> {
            new() {
                ParkingBoothId = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                BoothNumber = 2
            },
            new() {
                ParkingBoothId = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                BoothNumber = 2
            },
            new() {
                ParkingBoothId = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                BoothNumber = 3
            }
        };

        this.mockParkingBoothDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ParkingBooth>()).Returns(mockParkingBoothDbSet.Object);
        mockContext.Setup(c => c.ParkingBooths).Returns(mockParkingBoothDbSet.Object);

        this.sut = new ParkingBoothDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, "129d6427-adf2-4746-a33f-cfc60a51e4e2")]
    [DataRow(true, "029d6427-adf2-4746-a33f-cfc60a51e4e2")]
    public async Task Exists(bool expected, string id)
    {
        bool exists = await sut.ExistsAsync(new Guid(id));
        Assert.AreEqual(expected, exists);
    }
}
