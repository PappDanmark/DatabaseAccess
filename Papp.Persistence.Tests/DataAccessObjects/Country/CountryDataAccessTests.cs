using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

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

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.ExistsAsync((short) id);
        Assert.AreEqual(expected, exists);
    }
}
