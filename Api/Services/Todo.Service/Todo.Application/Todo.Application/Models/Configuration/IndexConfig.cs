namespace Todo.Application.Models.Configuration;

public class IndexConfig
{
    public int BatchSize { get; set; }
    public int Delay { get; set; }
    public string IndexAPI { get; set; } = string.Empty;
}
