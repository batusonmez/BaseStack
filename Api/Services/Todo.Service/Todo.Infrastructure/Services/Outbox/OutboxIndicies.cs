
using Todo.Application.Models.DTO;

namespace Todo.Infrastructure.Services.Outbox;

public static class OutboxIndicies
{
    public static string IndexName(this object source)
    {
        if (source is TodoDTO)
        {
            return "todo_ind";
        }

        return "";
    }
}
