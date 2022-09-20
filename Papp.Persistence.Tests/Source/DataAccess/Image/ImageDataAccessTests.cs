using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using System.Linq.Expressions;

namespace Papp.Persistence.Tests;

[TestClass]
public class ImageDataAccessTests
{
    private List<Image> mockData;
    private Mock<DbSet<Image>> mockImageDbSet;
    private Mock<PappDbContext> mockContext;
    private ImageDataAccess sut;

    public ImageDataAccessTests()
    {
        this.mockData = new List<Image> {
            new() {
                Id = 1,
                Name = "Image1",
                CompressionType = "gzip",
                MimeType = "jpeg"
            },
            new() {
                Id = 2,
                Name = "Image2",
                CompressionType = "gzip",
                MimeType = "jpeg"
            },
            new() {
                Id = 3,
                Name = "Image3",
                CompressionType = "zip",
                MimeType = "png"
            }
        };

        this.mockImageDbSet = mockData.AsQueryable().BuildMockDbSet();

        this.mockContext = new();
        mockContext.Setup(c => c.Set<Image>()).Returns(mockImageDbSet.Object);
        mockContext.Setup(c => c.Images).Returns(mockImageDbSet.Object);

        this.sut = new ImageDataAccess(mockContext.Object);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task AddAsync()
    {
        Image Image = new Mock<Image>().Object;

        // Run SUT
        await sut.AddAsync(Image);

        // Verify
        this.mockImageDbSet.Verify(c => c.AddAsync(It.IsAny<Image>(), default), Times.Once);
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetAllAsync()
    {
        // Implementation of the test
        Func<Expression<Func<Image, bool>>?, IList<Image>, Task> runner = async (filter, expected) =>
        {
            IList<Image> ImageList = await sut.GetAllAsync(filter);

            Assert.AreEqual(expected.Count, ImageList.Count);
            // If the retrieved subset of entities matches the expected number of entities,
            // proceed to compare each one, making sure the retrieved list is correct.
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], ImageList[i]);
            }
        };

        // Actual tests
        // Takes in the optional lamba exp. based on which to filter and the expected list of entities
        await runner(null, mockData);
        await runner(e => e.MimeType.Equals("jpeg"), mockData.Where(e => e.MimeType.Equals("jpeg")).ToList());
        await runner(e => e.Name.Equals("Image2"), new List<Image> { mockData[1] });
        await runner(e => e.Name.Equals("  "), new List<Image>());
    }

    [TestMethod]
    [TestCategory(TestConstants.UnitTest)]
    public async Task GetFirstOrDefaultAsync()
    {
        // Implementation of the test
        Func<Expression<Func<Image, bool>>, Image?, Task> runner = async (filter, expected) =>
        {
            Image? Image = await sut.GetFirstOrDefaultAsync(filter);
            if (expected == null)
            {
                Assert.IsNull(Image);
            }
            else
            {
                Assert.IsNotNull(Image);
                Assert.AreEqual(expected, Image);
            }
        };

        // Actual tests
        // Takes in the lamba exp. based on which to filter and the expected
        await runner(e => e.Id.Equals(-2), null);
        await runner(e => e.Id.Equals(0), null);
        await runner(e => e.Id.Equals(145), null);
        await runner(e => e.Id.Equals(2), mockData[1]);
        await runner(e => e.CompressionType.Equals("abcd"), null);
        await runner(e => e.CompressionType.Equals("gzip"), mockData[0]);
    }

    [DataTestMethod]
    [TestCategory(TestConstants.UnitTest)]
    [DataRow(false, -3)]
    [DataRow(true, 2)]
    public async Task Exists(bool expected, int id)
    {
        bool exists = await sut.Exists(id);
        Assert.AreEqual(expected, exists);
    }
}
