using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Country domain object.
/// </summary>
public interface ICountryDataAccess : IGenericDataAccess<Country>
{
    /// <summary>
    /// Updates a Country entity using the specified id.
    /// </summary>
    /// <param name="id">The Iso3166Numeric id of the Country which needs to be updated.</param>
    /// <param name="country">The new vesion of the Country used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Country entity, or null if a Country for the provided id doesn't exist.</returns>
    Country? Update(short id, Country country);

    /// <summary>
    /// Updates asynchronously a Country entity using the specified id.
    /// </summary>
    /// <param name="id">The Iso3166Numeric id of the Country which needs to be updated.</param>
    /// <param name="country">The new vesion of the Country used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Country entity, or null if a Country for the provided id doesn't exist.</returns>
    Task<Country?> UpdateAsync(short id, Country country);
}
