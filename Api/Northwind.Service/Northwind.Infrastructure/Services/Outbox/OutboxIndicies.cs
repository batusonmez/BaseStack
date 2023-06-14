using Person.Application.DTO;

namespace Northwind.Infrastructure.Services.Outbox
{
    public static class OutboxIndicies
    {
        public static string IndexName(this object source)
        {
            if(source is ProductsDTO)
            {
                return "person_ind";
            }

            return "";
        }
    }
}
