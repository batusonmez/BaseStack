namespace Northwind.Application.Models.DTO.Types
{
    /// <summary>
    /// Application DTO
    /// </summary>
    public interface IDTO
    {
        /// <summary>
        /// Map primary key field
        /// </summary>
        object IndexKey { get; }
         
        /// <summary>
        /// Create document index for search database
        /// </summary>
        bool IndexEnabled { get; }
    }
}
