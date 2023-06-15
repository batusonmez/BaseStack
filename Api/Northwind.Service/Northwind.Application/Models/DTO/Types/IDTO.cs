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
        object GetKey { get; }
    }
}
