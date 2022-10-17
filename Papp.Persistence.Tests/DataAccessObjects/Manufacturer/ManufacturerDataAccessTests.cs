using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ManufacturerDataAccessTests
{
    private List<Manufacturer> mockData;
    private Mock<DbSet<Manufacturer>> mockManufacturerDbSet;
    private Mock<PappDbContext> mockContext;
    private ManufacturerDataAccess sut;

    public ManufacturerDataAccessTests()
    {
        this.mockData = new List<Manufacturer> {
            new() {
                Id = 1,
                Name = "m1"
            },
            new() {
                Id = 2,
                Name = "m1"
            },
            new() {
                Id = 3,
                Name = "m3"
            }
        };

        this.mockManufacturerDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Manufacturer>()).Returns(mockManufacturerDbSet.Object);
        mockContext.Setup(c => c.Manufacturers).Returns(mockManufacturerDbSet.Object);

        this.sut = new ManufacturerDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -2)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync((short) id);
        Assert.AreEqual(expected, exists);
    }
}
