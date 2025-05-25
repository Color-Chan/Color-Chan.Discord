namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     Base class for all component builders.
///     Provides a common interface for setting the ID of the component.
/// </summary>
/// <typeparam name="T">The type of the derived builder.</typeparam>
public abstract class BaseComponentBuilder<T> where T : BaseComponentBuilder<T>
{
    /// <summary>
    ///     The ID of the component.
    /// </summary>
    protected int? Id;
    
    /// <summary>
    ///    Adds an Optional identifier to the component.
    /// </summary>
    /// <param name="id">The id of the component.</param>
    /// <returns>
    ///     The updated <see cref="BaseComponentBuilder{T}" />.
    /// </returns>
    public T WithId(int id)
    {
        Id = id;
        return (T)this;
    }
}