using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class CountryDataAccessTests
{
    private List<Country> mockData;
    private Mock<DbSet<Country>> mockCountryDbSet;
    private Mock<PappDbContext> mockContext;
    private CountryDataAccess sut;

    public CountryDataAccessTests()
    {
        this.mockData = new List<Country> {
            new() {
                Iso3166Numeric = 1,
                CommonName = "Country#1",
                Population = 100
            },
            new() {
                Iso3166Numeric = 2,
                CommonName = "Country#2",
                Population = 100
            },
            new() {
                Iso3166Numeric = 3,
                CommonName = "Country#3",
                Population = 250
            }
        };

        this.mockCountryDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Country>()).Returns(mockCountryDbSet.Object);
        mockContext.Setup(c => c.Countries).Returns(mockCountryDbSet.Object);

        this.sut = new CountryDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Country Country = new Mock<Country>().Object;

        // Run SUT
        await sut.AddAsync(Country);

        // Verify
        this.mockCountryDbSet.Verify(c => c.AddAsync(It.IsAny<Country>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Country>?, IList<Country>, Task> runner = async (filter, expected) =>
        {
            IList<Country> CountryList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, CountryList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], CountryList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Country>(e => e.Population.Equals(100)), mockData.Where(e => e.Population.Equals(100)).ToList());
        await runner(new Specification<Country>(e => e.Iso3166Numeric.Equals(1)), new List<Country> { mockData[0] });
        await runner(new Specification<Country>(e => e.Iso3166Numeric.Equals(-3)), new List<Country>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Country>, Country?, Task> runner = async (filter, expected) =>
        {
            Country? Country = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Country);
            }
            else
            {
                Assert.IsNotNull(Country);
                Assert.AreEqual(expected, Country);
            }
        };

        // Actual tests
        // Takes in the specification class based on which to filter and the expected
        await runner(new Specification<Country>(e => e.Iso3166Numeric.Equals(-2)), null);
        await runner(new Specification<Country>(e => e.Iso3166Numeric.Equals(1)), mockData[0]);
        await runner(new Specification<Country>(e => e.CommonName.Equals("")), null);
        await runner(new Specification<Country>(e => e.CommonName.Equals("  ")), null);
        await runner(new Specification<Country>(e => e.CommonName.Equals("Country#3")), mockData[2]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists((short) id);
        Assert.AreEqual(expected, exists);
    }
}
