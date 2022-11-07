using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ZipCodeDataAccess : GenericDataAccess<ZipCode>, IZipCodeDataAccess
{
    private readonly PappDbContext DbContext;

    public ZipCodeDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ZipCodeDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public ZipCode? Update(int id, ZipCode zipCode)
    {
        var existing = DbContext.ZipCodes.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Code = zipCode.Code;
        existing.Name = zipCode.Name;
        existing.CountryId = zipCode.CountryId;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<ZipCode?> UpdateAsync(int id, ZipCode zipCode)
    {
        var existing = await DbContext.ZipCodes.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Code = zipCode.Code;
        existing.Name = zipCode.Name;
        existing.CountryId = zipCode.CountryId;

        return existing;
    }
}
