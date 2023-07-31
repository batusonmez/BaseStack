using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Queries.GenericQueries.Extansions.IndexQuery
{
    internal static class MapToKeyType
    {
        public static IEnumerable<object>? ToPrimaryKeyType<T>(this IEnumerable<string>? source) where T : class
        {
            if (source == null)
            {
                return null;
            }
            if (!source.Any())
            {
                return null;
            }            
    
                        
            IDTO? mock = Activator.CreateInstance(typeof(T)) as IDTO;
            if (mock == null)
            {
                return null;
            }
            if (mock.ID == null)
            {
                return null;
            }

            TypeCode idType= Type.GetTypeCode( mock.ID.GetType());
            
            
            switch (idType)
            {
                case TypeCode.Int32:
                    return source.Cast<int>().Select(d=>d as object);
                    
                default:
                    break;
            }
             
            return null;
        }
    }
}
