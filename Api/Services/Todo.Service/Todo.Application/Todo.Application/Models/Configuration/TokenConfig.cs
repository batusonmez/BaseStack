namespace Todo.Application.Models.Configuration;

public class TokenConfig
{
    public string? MetaURL { get; set; }
    public string? Authority { get; set; }
    public string? Audience { get; set; }
    public string? TokenURL { get; set; }
    public string? ClientID { get; set; }
    public string? ClientSecret { get; set; }
    public string? GrantType { get; set; }

    public bool IsValid
    {
        get
        {
            return !(string.IsNullOrEmpty(TokenURL) || string.IsNullOrEmpty(ClientID) || string.IsNullOrEmpty(ClientSecret) || string.IsNullOrEmpty(GrantType));
        }
    }

}
