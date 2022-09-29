using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class ChargerTypeDataAccessTests
{
    private List<ChargerType> mockData;
    private Mock<DbSet<ChargerType>> mockChargerTypeDbSet;
    private Mock<PappDbContext> mockContext;
    private ChargerTypeDataAccess sut;

    public ChargerTypeDataAccessTests()
    {
        this.mockData = new List<ChargerType> {
            new() {
                Id = 1,
                Operator = 2,
                Kilowatt = 25,
                Dc = false,
                Name = "ChT#1"
            },
            new() {
                Id = 2,
                Operator = 2,
                Kilowatt = 25,
                Dc = true,
                Name = "ChT#2"
            },
            new() {
                Id = 3,
                Operator = 3,
                Kilowatt = 20,
                Dc = true,
                Name = "ChT#3"
            }
        };

        this.mockChargerTypeDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<ChargerType>()).Returns(mockChargerTypeDbSet.Object);
        mockContext.Setup(c => c.ChargerTypes).Returns(mockChargerTypeDbSet.Object);

        this.sut = new ChargerTypeDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        ChargerType ChargerType = new Mock<ChargerType>().Object;

        // Run SUT
        await sut.AddAsync(ChargerType);

        // Verify
        this.mockChargerTypeDbSet.Verify(c => c.AddAsync(It.IsAny<ChargerType>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ChargerType>?, IList<ChargerType>, Task> runner = async (filter, expected) =>
        {
            IList<ChargerType> ChargerTypeList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ChargerTypeList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ChargerTypeList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<ChargerType>(e => e.Operator.Equals(2)), mockData.Where(e => e.Operator.Equals(2)).ToList());
        await runner(new Specification<ChargerType>(e => e.Kilowatt.Equals(20)), new List<ChargerType> { mockData[2] });
        await runner(new Specification<ChargerType>(e => e.Kilowatt.Equals(-3)), new List<ChargerType>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ChargerType>, ChargerType?, Task> runner = async (filter, expected) =>
        {
            ChargerType? ChargerType = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(ChargerType);
            }
            else
            {
                Assert.IsNotNull(ChargerType);
                Assert.AreEqual(expected, ChargerType);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<ChargerType>(e => e.Name.ToString().Equals("")), null);
        await runner(new Specification<ChargerType>(e => e.Name.ToString().Equals("   ")), null);
        await runner(new Specification<ChargerType>(e => e.Id.Equals(-4)), null);
        await runner(new Specification<ChargerType>(e => e.Id.Equals(3)), mockData[2]);
        await runner(new Specification<ChargerType>(e => e.Operator.Equals(-1)), null);
        await runner(new Specification<ChargerType>(e => e.Operator.Equals(2)), mockData[0]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -4)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
