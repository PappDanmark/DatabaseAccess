using System.Linq.Expressions;
using Papp.Domain;
using Papp.Persistence.Tests.Data;
using Xunit;

namespace Papp.Persistence.Tests;

public class GenericDataAccessDataSource
{
    // Defines the data to be tested for GetFirstOrDefault method.
    // First argument is the expected, the rest correspond to the test subject's arguments.
    public static TheoryData<Booth?, Expression<Func<Booth, bool>>, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>?, bool> GetFirstOrDefault => new()
    {
        { null, e => e.Id.ToString().Equals(""), null, false },
        { null, e => e.Id.ToString().Equals("   "), null, false  },
        { null, e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2"), null, false },
        { PappDbDataSet.Booths[1], e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2"), null, false },
        { PappDbDataSet.Booths[2], e => true, e => e.OrderByDescending(x => x.BoothNumber), false },
        { null, e => e.BoothNumber.Equals(-1), null, false },
        { PappDbDataSet.Booths[2], e => e.BoothNumber.Equals(3), null, false }
    };

    public static TheoryData<Booth?, Expression<Func<Booth, bool>>, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>?, bool> GetFirstOrDefaultAsync => new()
    {
        { null, e => e.Id.ToString().Equals(""), null, false },
        { null, e => e.Id.ToString().Equals("   "), null, false },
        { null, e => e.Id.ToString().Equals("129d6427-adf2-4746-a33f-cfc60a51e4e2"), null, false },
        { PappDbDataSet.Booths[1], e => e.Id.ToString().Equals("029d6427-adf2-4746-a33f-cfc60a51e4e2"), null, false },
        { PappDbDataSet.Booths[2], e => true, e => e.OrderByDescending(x => x.BoothNumber), false },
        { null, e => e.BoothNumber.Equals(-1), null, false },
        { PappDbDataSet.Booths[2], e => e.BoothNumber.Equals(3), null, false }
    };

    public static TheoryData<ICollection<Booth>, Expression<Func<Booth, bool>>?, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>?, bool> GetAllAsEnumerable => new()
    {
        { PappDbDataSet.Booths, null, null, false },
        { PappDbDataSet.Booths.Where(e => e.MuncipalityId.Equals("m2")).ToList(), e => e.MuncipalityId.Equals("m2"), null, false },
        { new Booth[] {PappDbDataSet.Booths[0]}, e => e.BoothNumber.Equals(1), null, false },
        { Array.Empty<Booth>(), e => e.BoothNumber.Equals(-3), null, false }
    };

    public static TheoryData<ICollection<Booth>, Expression<Func<Booth, bool>>?, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>?, bool> GetAllAsCollection => new()
    {
        { PappDbDataSet.Booths, null, null, false },
        { PappDbDataSet.Booths.Where(e => e.MuncipalityId.Equals("m2")).ToList(), e => e.MuncipalityId.Equals("m2"), null, false },
        { new Booth[] {PappDbDataSet.Booths[0]}, e => e.BoothNumber.Equals(1), null, false },
        { Array.Empty<Booth>(), e => e.BoothNumber.Equals(-3), null, false }
    };

    public static TheoryData<ICollection<Booth>, Expression<Func<Booth, bool>>?, Func<IQueryable<Booth>, IOrderedQueryable<Booth>>?, bool> GetAllAsCollectionAsync => new()
    {
        { PappDbDataSet.Booths, null, null, false },
        { PappDbDataSet.Booths.Where(e => e.MuncipalityId.Equals("m2")).ToList(), e => e.MuncipalityId.Equals("m2"), null, false },
        { new Booth[] {PappDbDataSet.Booths[0]}, e => e.BoothNumber.Equals(1), null, false },
        { Array.Empty<Booth>(), e => e.BoothNumber.Equals(-3), null, false }
    };
}
