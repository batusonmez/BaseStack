using Todo.Application.Models.DTO.Types;

namespace Todo.Application.Models.DTO;

public class TodoDTO : IDTO
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? DeleteDate { get; set; }

    public object IndexKey => "Todo";

    public bool IndexEnabled => true;

    public bool HasID => ID != default(Guid);

    public TodoDTO()
    {
        Title = string.Empty;
    }
}
