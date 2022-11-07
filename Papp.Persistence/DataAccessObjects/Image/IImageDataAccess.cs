using Papp.Domain;

namespace Papp.Persistence.DataAccess;

/// <summary>
/// Interface defining specific methods for the Image domain object.
/// </summary>
public interface IImageDataAccess : IGenericDataAccess<Image>
{
    /// <summary>
    /// Updates a Image entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Image which needs to be updated.</param>
    /// <param name="image">The new vesion of the Image used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Image entity, or null if a Image for the provided id doesn't exist.</returns>
    Image? Update(int id, Image image);

    /// <summary>
    /// Updates asynchronously a Image entity using the specified id.
    /// </summary>
    /// <param name="id">The id of the Image which needs to be updated.</param>
    /// <param name="image">The new vesion of the Image used to update the information.</param>
    /// <remarks>Doesn't update the Id field.</remarks>
    /// <returns>The new modified Image entity, or null if a Image for the provided id doesn't exist.</returns>
    Task<Image?> UpdateAsync(int id, Image image);
}
