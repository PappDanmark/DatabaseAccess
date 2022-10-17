using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ParkingAreaTransactionDataAccessTests
{
    private List<ParkingAreaTransaction> mockData;
    private Mock<DbSet<ParkingAreaTransaction>> mockParkingAreaTransactionDbSet;
    private Mock<PappDbContext> mockContext;
    private ParkingAreaTransactionDataAccess sut;

    public ParkingAreaTransactionDataAccessTests()
    {
        this.mockData = new List<ParkingAreaTransaction> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                ParkingAreaId = 2
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                ParkingAreaId = 2
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                ParkingAreaId = 3
            }
        };

        this.mockParkingAreaTransactionDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ParkingAreaTransaction>()).Returns(mockParkingAreaTransactionDbSet.Object);
        mockContext.Setup(c => c.ParkingAreaTransactions).Returns(mockParkingAreaTransactionDbSet.Object);

        this.sut = new ParkingAreaTransactionDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        ParkingAreaTransaction ParkingAreaTransaction = new Mock<ParkingAreaTransaction>().Object;

        // Run SUT
        await sut.AddAsync(ParkingAreaTransaction);

        // Verify
        this.mockParkingAreaTransactionDbSet.Verify(c => c.AddAsync(It.IsAny<ParkingAreaTransaction>(), default), Times.Once);
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
