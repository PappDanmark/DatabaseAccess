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

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        ZipCode ZipCode = new Mock<ZipCode>().Object;

        // Run SUT
        await sut.AddAsync(ZipCode);

        // Verify
        this.mockZipCodeDbSet.Verify(c => c.AddAsync(It.IsAny<ZipCode>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ZipCode>?, IList<ZipCode>, Task> runner = async (filter, expected) =>
        {
            IList<ZipCode> ZipCodeList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ZipCodeList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ZipCodeList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<ZipCode>(e => e.CountryId.Equals(2)), mockData.Where(e => e.CountryId.Equals(2)).ToList());
        await runner(new Specification<ZipCode>(e => e.Code.Equals("4321")), new List<ZipCode> { mockData[2] });
        await runner(new Specification<ZipCode>(e => e.Code.Equals("")), new List<ZipCode>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<ZipCode>, ZipCode?, Task> runner = async (filter, expected) =>
        {
            ZipCode? ZipCode = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(ZipCode);
            }
            else
            {
                Assert.IsNotNull(ZipCode);
                Assert.AreEqual(expected, ZipCode);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<ZipCode>(e => e.Id.Equals(-2)), null);
        await runner(new Specification<ZipCode>(e => e.Id.Equals(-1)), null);
        await runner(new Specification<ZipCode>(e => e.Id.Equals(0)), null);
        await runner(new Specification<ZipCode>(e => e.Id.Equals(2)), mockData[1]);
        await runner(new Specification<ZipCode>(e => e.CountryId.Equals(-1)), null);
        await runner(new Specification<ZipCode>(e => e.CountryId.Equals(3)), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -2)]
    [DataRow(true, 1)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
