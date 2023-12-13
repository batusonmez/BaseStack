namespace Todo.Application.Models.DTO.Types;

/// <summary>
/// Application DTO
/// </summary>
public interface IDTO
{
    /// <summary>
    /// Index name
    /// </summary>
    object IndexKey { get; }

    /// <summary>
    /// Create document index for search database
    /// </summary>
    bool IndexEnabled { get; }

    /// <summary>
    /// Gets if object has a database primary key
    /// </summary>
    bool HasID { get; }

    /// <summary>
    /// Uniqe object key
    /// </summary>
    Guid ID { get; }
 
}
