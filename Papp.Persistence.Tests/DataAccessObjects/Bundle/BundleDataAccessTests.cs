using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class BundleDataAccessTests
{
    private List<Bundle> mockData;
    private Mock<DbSet<Bundle>> mockBundleDbSet;
    private Mock<PappDbContext> mockContext;
    private BundleDataAccess sut;

    public BundleDataAccessTests()
    {
        this.mockData = new List<Bundle> {
            new() {
                Id = 1,
                Address = "address#1",
                Zip = 8700
            },
            new() {
                Id = 2,
                Address = "address#2",
                Zip = 8700
            },
            new() {
                Id = 3,
                Address = "address#3",
                Zip = 8800
            }
        };

        this.mockBundleDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Bundle>()).Returns(mockBundleDbSet.Object);
        mockContext.Setup(c => c.Bundles).Returns(mockBundleDbSet.Object);

        this.sut = new BundleDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -5)]
    [DataRow(false, 0)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync(id);
        Assert.AreEqual(expected, exists);
    }
}
