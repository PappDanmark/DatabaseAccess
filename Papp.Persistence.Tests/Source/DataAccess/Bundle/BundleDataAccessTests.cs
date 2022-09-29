using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;

namespace Papp.Persistence.Tests;

[TestClass]
public class BundleDataAccessTests
{
    private List<Bundle> mockData;
    private Mock<DbSet<Bundle>> mockBundleDbSet;
    private Mock<PappDbContext> mockContext;
    private BundleDataAccess sut;

    public BundleDataAccessTests()
    {
        this.mockData = new List<Bundle> {
            new() {
                Id = 1,
                Address = "address#1",
                Zip = 8700
            },
            new() {
                Id = 2,
                Address = "address#2",
                Zip = 8700
            },
            new() {
                Id = 3,
                Address = "address#3",
                Zip = 8800
            }
        };

        this.mockBundleDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Bundle>()).Returns(mockBundleDbSet.Object);
        mockContext.Setup(c => c.Bundles).Returns(mockBundleDbSet.Object);

        this.sut = new BundleDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Bundle Bundle = new Mock<Bundle>().Object;

        // Run SUT
        await sut.AddAsync(Bundle);

        // Verify
        this.mockBundleDbSet.Verify(c => c.AddAsync(It.IsAny<Bundle>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Bundle>?, IList<Bundle>, Task> runner = async (filter, expected) =>
        {
            IList<Bundle> BundleList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, BundleList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], BundleList[i]);
            }
        };

        // Actual tests
        // Takes in the optional specification class based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(new Specification<Bundle>(e => e.Zip.Equals(8700)), mockData.Where(e => e.Zip.Equals(8700)).ToList());
        await runner(new Specification<Bundle>(e => e.Id.Equals(2)), new List<Bundle> { mockData[1] });
        await runner(new Specification<Bundle>(e => e.Id.Equals(-3)), new List<Bundle>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<IBaseSpecification<Bundle>, Bundle?, Task> runner = async (filter, expected) =>
        {
            Bundle? Bundle = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Bundle);
            }
            else
            {
                Assert.IsNotNull(Bundle);
                Assert.AreEqual(expected, Bundle);
            }
        };

        // Actual tests
        // Takes in a specification class based on which to filter and the expected
        await runner(new Specification<Bundle>(e => e.Id.Equals(-2)), null);
        await runner(new Specification<Bundle>(e => e.Id.Equals(0)), null);
        await runner(new Specification<Bundle>(e => e.Id.Equals(142)), null);
        await runner(new Specification<Bundle>(e => e.Id.Equals(3)), mockData[2]);
        await runner(new Specification<Bundle>(e => e.Zip.Equals(-1)), null);
        await runner(new Specification<Bundle>(e => e.Address.Equals("address#2")), mockData[1]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -5)]
    [DataRow(false, 0)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
