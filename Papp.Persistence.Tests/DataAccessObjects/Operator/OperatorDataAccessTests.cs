using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

namespace Papp.Persistence.Tests;

[TestClass]
public class OperatorDataAccessTests
{
    private List<Operator> mockData;
    private Mock<DbSet<Operator>> mockOperatorDbSet;
    private Mock<PappDbContext> mockContext;
    private OperatorDataAccess sut;

    public OperatorDataAccessTests()
    {
        this.mockData = new List<Operator> {
            new() {
                Id = 1,
                Name = "op#1"
            },
            new() {
                Id = 2,
                Name = "op#2"
            },
            new() {
                Id = 3,
                Name = "op#2"
            }
        };

        this.mockOperatorDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Operator>()).Returns(mockOperatorDbSet.Object);
        mockContext.Setup(c => c.Operators).Returns(mockOperatorDbSet.Object);

        this.sut = new OperatorDataAccess(mockContext.Object);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync((short) id);
        Assert.AreEqual(expected, exists);
    }
}
