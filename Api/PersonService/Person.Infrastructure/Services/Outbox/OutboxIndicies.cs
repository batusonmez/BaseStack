using Person.Application.DTO;

namespace Person.Infrastructure.Services.Outbox
{
    public static class OutboxIndicies
    {
        public static string IndexName(this object source)
        {
            if(source is PersonDTO)
            {
                return "person_ind";
            }

            return "";
        }
    }
}
