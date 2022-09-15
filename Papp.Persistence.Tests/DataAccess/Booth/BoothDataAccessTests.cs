using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using System.Linq.Expressions;

namespace Papp.Persistence.Tests;

[TestClass]
public class BoothDataAccessTests
{
    private Mock<DbSet<Booth>> mockBoothDbSet;
    private Mock<PappDbContext> mockContext;
    private BoothDataAccess sut;

    public BoothDataAccessTests()
    {
        this.mockBoothDbSet = new List<Booth> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                BoothNumber = 1
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                BoothNumber = 2
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                BoothNumber = 3,
                BundleNavigation = new()
            }
        }.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Booth>()).Returns(mockBoothDbSet.Object);
        mockContext.Setup(c => c.Booths).Returns(mockBoothDbSet.Object);

        this.sut = new BoothDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Booth booth = new Mock<Booth>().Object;

        // Run SUT
        await sut.AddAsync(booth);

        // Verify
        this.mockBoothDbSet.Verify(c => c.AddAsync(It.IsAny<Booth>(), default), Times.Once);
        this.mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Run SUT
        IList<Booth> booths = await sut.GetAllAsync();

        // Verify
        Assert.AreEqual(3, booths.Count);
        Assert.AreEqual(1, booths[0].BoothNumber);
        Assert.AreEqual(2, booths[1].BoothNumber);
        Assert.AreEqual(3, booths[2].BoothNumber);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow("", false)]
    [DataRow("   ", false)]
    [DataRow("129d6427-adf2-4746-a33f-cfc60a51e4e2", false)]
    [DataRow("029d6427-adf2-4746-a33f-cfc60a51e4e2", true)]
    public async Task GetFirstOrDefaultAsync(string id, bool expected)
    {
        Booth? booth = await sut.GetFirstOrDefaultAsync(e => e.Id.ToString().Equals(id));

        if (expected)
        {
            Assert.IsNotNull(booth);
            Assert.AreEqual(id, booth.Id.ToString());
        }
        else
        {
            Assert.IsNull(booth);
        }
    }
}
