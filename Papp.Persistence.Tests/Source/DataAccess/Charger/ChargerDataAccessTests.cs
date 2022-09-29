using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class ChargerDataAccessTests
{
    private List<Charger> mockData;
    private Mock<DbSet<Charger>> mockChargerDbSet;
    private Mock<PappDbContext> mockContext;
    private ChargerDataAccess sut;

    public ChargerDataAccessTests()
    {
        this.mockData = new List<Charger> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                OperatorId = "#1",
                ChargerType = 2
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                OperatorId = "#2",
                ChargerType = 3
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                OperatorId = "#3",
                ChargerType = 2
            }
        };

        this.mockChargerDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Charger>()).Returns(mockChargerDbSet.Object);
        mockContext.Setup(c => c.Chargers).Returns(mockChargerDbSet.Object);

        this.sut = new ChargerDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Charger Charger = new Mock<Charger>().Object;

        // Run SUT
        await sut.AddAsync(Charger);

        // Verify
        this.mockChargerDbSet.Verify(c => c.AddAsync(It.IsAny<Charger>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Charger>?, IList<Charger>, Task> runner = async (filter, expected) =>
        {
            IList<Charger> ChargerList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ChargerList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ChargerList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Charger>(e => e.OperatorId.Equals("#2")), mockData.Where(e => e.OperatorId.Equals("#2")).ToList());
        await runner(new Specification<Charger>(e => e.ChargerType.Equals(3)), new List<Charger> { mockData[1] });
        await runner(new Specification<Charger>(e => e.ChargerType.Equals(-3)), new List<Charger>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Charger>, Charger?, Task> runner = async (filter, expected) =>
        {
            Charger? Charger = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Charger);
            }
            else
            {
                Assert.IsNotNull(Charger);
                Assert.AreEqual(expected, Charger);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<Charger>(e => e.Id.ToString().Equals("")), null);
        await runner(new Specification<Charger>(e => e.Id.ToString().Equals("   ")), null);
        await runner(new Specification<Charger>(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2")), null);
        await runner(new Specification<Charger>(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")), mockData[1]);
        await runner(new Specification<Charger>(e => e.OperatorId.Equals("abcd")), null);
        await runner(new Specification<Charger>(e => e.ChargerType.Equals(2)), mockData[0]);
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
