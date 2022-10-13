using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ZipCodeDataAccessTests
{
    private List<ZipCode> mockData;
    private Mock<DbSet<ZipCode>> mockZipCodeDbSet;
    private Mock<PappDbContext> mockContext;
    private ZipCodeDataAccess sut;

    public ZipCodeDataAccessTests()
    {
        this.mockData = new List<ZipCode> {
            new() {
                Id = 1,
                Code = "1234",
                CountryId = 2
            },
            new() {
                Id = 2,
                Code = "1234",
                CountryId = 2
            },
            new() {
                Id = 3,
                Code = "4321",
                CountryId = 3
            }
        };

        this.mockZipCodeDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ZipCode>()).Returns(mockZipCodeDbSet.Object);
        mockContext.Setup(c => c.ZipCodes).Returns(mockZipCodeDbSet.Object);

        this.sut = new ZipCodeDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -2)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync(id);
        Assert.AreEqual(expected, exists);
    }
}
