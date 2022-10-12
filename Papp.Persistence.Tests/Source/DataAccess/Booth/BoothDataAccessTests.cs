using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.Context;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class BoothDataAccessTests
{
    private List<Booth> mockData;
    private Mock<DbSet<Booth>> mockBoothDbSet;
    private Mock<PappDbContext> mockContext;
    private BoothDataAccess sut;

    public BoothDataAccessTests()
    {
        this.mockData = new List<Booth> {
            new() {
                Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
                BoothNumber = 1,
                MuncipalityId = "m2"
            },
            new() {
                Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
                BoothNumber = 2,
                MuncipalityId = "m2"
            },
            new() {
                Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
                BoothNumber = 3,
                MuncipalityId = "m3"
            }
        };

        this.mockBoothDbSet = mockData.AsQueryable().BuildMockDbSet();

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
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Booth>?, IList<Booth>, Task> runner = async (filter, expected) =>
        {
            IList<Booth> boothList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, boothList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], boothList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Booth>(e => e.MuncipalityId.Equals("m2")), mockData.Where(e => e.MuncipalityId.Equals("m2")).ToList());
        await runner(new Specification<Booth>(e => e.BoothNumber.Equals(1)), new List<Booth> { mockData[0] });
        await runner(new Specification<Booth>(e => e.BoothNumber.Equals(-3)), new List<Booth>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Booth>, Booth?, Task> runner = async (filter, expected) =>
        {
            Booth? booth = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(booth);
            }
            else
            {
                Assert.IsNotNull(booth);
                Assert.AreEqual(expected, booth);
            }
        };

        // Actual tests
        // Takes in a specification class based on which to filter and the expected
        await runner(new Specification<Booth>(e => e.Id.ToString().Equals("")), null);
        await runner(new Specification<Booth>(e => e.Id.ToString().Equals("   ")), null);
        await runner(new Specification<Booth>(e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2")), null);
        await runner(new Specification<Booth>(e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2")), mockData[1]);
        await runner(new Specification<Booth>(e => e.BoothNumber.Equals(-1)), null);
        await runner(new Specification<Booth>(e => e.BoothNumber.Equals(3)), mockData[2]);
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
