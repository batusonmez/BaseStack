using Person.Domain.DTO;

namespace Person.Domain.Services.Outbox
{
    public static class OutboxIndicies
    {
        public static string IndexName(this object source)
        {
            if(source is PersonDTO)
            {
                return "PersonInd";
            }

            return "";
        }
    }
}
