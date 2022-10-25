using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Papp.Domain;
using Papp.Persistence.DataAccess;
using Papp.Persistence.Context;

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
}
