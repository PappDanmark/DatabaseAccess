using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

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

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Operator Operator = new Mock<Operator>().Object;

        // Run SUT
        await sut.AddAsync(Operator);

        // Verify
        this.mockOperatorDbSet.Verify(c => c.AddAsync(It.IsAny<Operator>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Operator>?, IList<Operator>, Task> runner = async (filter, expected) =>
        {
            IList<Operator> OperatorList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, OperatorList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], OperatorList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Operator>(e => e.Name.Equals("op#2")), mockData.Where(e => e.Name.Equals("op#2")).ToList());
        await runner(new Specification<Operator>(e => e.Id.Equals(1)), new List<Operator> { mockData[0] });
        await runner(new Specification<Operator>(e => e.Id.Equals(-3)), new List<Operator>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Operator>, Operator?, Task> runner = async (filter, expected) =>
        {
            Operator? Operator = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Operator);
            }
            else
            {
                Assert.IsNotNull(Operator);
                Assert.AreEqual(expected, Operator);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<Operator>(e => e.Id.Equals(-3)), null);
        await runner(new Specification<Operator>(e => e.Id.Equals(2)), mockData[1]);
        await runner(new Specification<Operator>(e => e.Name.Equals("")), null);
        await runner(new Specification<Operator>(e => e.Name.Equals("op#1")), mockData[0]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists((short) id);
        Assert.AreEqual(expected, exists);
    }
}
