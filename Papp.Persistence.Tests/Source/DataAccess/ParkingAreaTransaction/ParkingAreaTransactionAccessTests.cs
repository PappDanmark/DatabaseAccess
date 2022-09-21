using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using System.Linq.Expressions;

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

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<Expression<Func<ParkingAreaTransaction, bool>>?, IList<ParkingAreaTransaction>, Task> runner = async (filter, expected) =>
        {
            IList<ParkingAreaTransaction> ParkingAreaTransactionList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ParkingAreaTransactionList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ParkingAreaTransactionList[i]);
            }
        };

        // Actual tests
        // Takes in the optional lamba exp. based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(e => e.ParkingAreaId.Equals(2), mockData.Where(e => e.ParkingAreaId.Equals(2)).ToList());
        await runner(e => e.ParkingAreaId.Equals(3), new List<ParkingAreaTransaction> { mockData[2] });
        await runner(e => e.ParkingAreaId.Equals(-3), new List<ParkingAreaTransaction>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<Expression<Func<ParkingAreaTransaction, bool>>, ParkingAreaTransaction?, Task> runner = async (filter, expected) =>
        {
            ParkingAreaTransaction? ParkingAreaTransaction = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(ParkingAreaTransaction);
            }
            else
            {
                Assert.IsNotNull(ParkingAreaTransaction);
                Assert.AreEqual(expected, ParkingAreaTransaction);
            }
        };

        // Actual tests
        // Takes in the lamba exp. based on which to filter and the expected
        await runner(e => e.Id.ToString().Equals(""), null);
        await runner(e => e.Id.ToString().Equals("   "), null);
        await runner(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2"), null);
        await runner(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2"), mockData[1]);
        await runner(e => e.ParkingAreaId.Equals(-1), null);
        await runner(e => e.ParkingAreaId.Equals(3), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, "129d6427-adf2-4746-a33f-cfc60a51e4e2")]
    [DataRow(true, "029d6427-adf2-4746-a33f-cfc60a51e4e2")]
    public async Task Exists(bool expected, string id)
    {
        bool exists = await sut.Exists(new Guid(id));
        Assert.AreEqual(expected, exists);
    }
}
