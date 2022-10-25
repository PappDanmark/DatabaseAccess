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
}
