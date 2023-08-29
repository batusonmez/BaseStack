namespace Northwind.Application.Services.Token
{
    public interface ITokenService
    {
        Task<string?> GetSampleToken();
    }
}
