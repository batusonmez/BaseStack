namespace Todo.Domain.Entities;

public class Outbox
{
    public Guid ID { get; set; }
    public DateTime CreationDate { get; set; }
    public string? DataType { get; set; }
    public string? Data { get; set; }
    public DateTime? RequestDate { get; set; }
    public DateTime? ProcessDate { get; set; }
    public string DataID { get; set; }
    public Outbox()
    {
        CreationDate = DateTime.Now;
        DataID = string.Empty;
    }


}
