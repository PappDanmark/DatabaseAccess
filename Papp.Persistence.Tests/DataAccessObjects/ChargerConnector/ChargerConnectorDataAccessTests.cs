using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class ChargerConnectorDataAccessTests
{
    private List<ChargerConnector> mockData;
    private Mock<DbSet<ChargerConnector>> mockChargerConnectorDbSet;
    private Mock<PappDbContext> mockContext;
    private ChargerConnectorDataAccess sut;

    public ChargerConnectorDataAccessTests()
    {
        this.mockData = new List<ChargerConnector> {
            new() {
                Id = 1,
                Name = "#1"
            },
            new() {
                Id = 2,
                Name = "#2"
            },
            new() {
                Id = 3,
                Name = "#2"
            }
        };

        this.mockChargerConnectorDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ChargerConnector>()).Returns(mockChargerConnectorDbSet.Object);
        mockContext.Setup(c => c.ChargerConnectors).Returns(mockChargerConnectorDbSet.Object);

        this.sut = new ChargerConnectorDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -4)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync((short) id);
        Assert.AreEqual(expected, exists);
    }
}
