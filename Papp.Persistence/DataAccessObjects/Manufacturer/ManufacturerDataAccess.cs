using Microsoft.EntityFrameworkCore;
using Papp.Domain;
using Papp.Persistence.Context;

namespace Papp.Persistence.DataAccess;

/// <inheritdoc/>
public class ManufacturerDataAccess : GenericDataAccess<Manufacturer>, IManufacturerDataAccess
{
    private readonly PappDbContext DbContext;

    public ManufacturerDataAccess(PappDbContext context) : base(context)
    {
        this.DbContext = context;
    }

    public ManufacturerDataAccess(IUnitOfWork<PappDbContext> unitOfWork): base(unitOfWork)
    {
        this.DbContext = unitOfWork.DbContext;
    }

    /// <inheritdoc/>
    public Manufacturer? Update(short id, Manufacturer manufacturer)
    {
        var existing = DbContext.Manufacturers.FirstOrDefault(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = manufacturer.Name;

        return existing;
    }

    /// <inheritdoc/>
    public async Task<Manufacturer?> UpdateAsync(short id, Manufacturer manufacturer)
    {
        var existing = await DbContext.Manufacturers.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Name = manufacturer.Name;

        return existing;
    }
}
