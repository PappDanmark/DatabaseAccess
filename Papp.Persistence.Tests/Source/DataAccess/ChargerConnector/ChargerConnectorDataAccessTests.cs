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

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        ChargerConnector ChargerConnector = new Mock<ChargerConnector>().Object;

        // Run SUT
        await sut.AddAsync(ChargerConnector);

        // Verify
        this.mockChargerConnectorDbSet.Verify(c => c.AddAsync(It.IsAny<ChargerConnector>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ChargerConnector>?, IList<ChargerConnector>, Task> runner = async (filter, expected) =>
        {
            IList<ChargerConnector> ChargerConnectorList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ChargerConnectorList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ChargerConnectorList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<ChargerConnector>(e => e.Name.Equals("#2")), mockData.Where(e => e.Name.Equals("#2")).ToList());
        await runner(new Specification<ChargerConnector>(e => e.Id.Equals(1)), new List<ChargerConnector> { mockData[0] });
        await runner(new Specification<ChargerConnector>(e => e.Id.Equals(-3)), new List<ChargerConnector>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ChargerConnector>, ChargerConnector?, Task> runner = async (filter, expected) =>
        {
            ChargerConnector? ChargerConnector = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(ChargerConnector);
            }
            else
            {
                Assert.IsNotNull(ChargerConnector);
                Assert.AreEqual(expected, ChargerConnector);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<ChargerConnector>(e => e.Id.Equals(-1)), null);
        await runner(new Specification<ChargerConnector>(e => e.Id.Equals(0)), null);
        await runner(new Specification<ChargerConnector>(e => e.Id.Equals(144)), null);
        await runner(new Specification<ChargerConnector>(e => e.Id.Equals(2)), mockData[1]);
        await runner(new Specification<ChargerConnector>(e => e.Name.Equals(" ")), null);
        await runner(new Specification<ChargerConnector>(e => e.Name.Equals("")), null);
        await runner(new Specification<ChargerConnector>(e => e.Name.Equals("#2")), mockData[1]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -4)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists((short) id);
        Assert.AreEqual(expected, exists);
    }
}
