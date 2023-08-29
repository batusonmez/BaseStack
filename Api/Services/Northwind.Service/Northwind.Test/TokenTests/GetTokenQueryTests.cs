using Moq;
using Northwind.Application.Services.Token;
using Northwind.Application.Queries.Token.GetToken;

namespace Northwind.Test.TokenTests
{
    [TestClass]
    public class GetTokenQueryTests : BaseTest
    {
        public ITokenService MockTokenService(IEnumerable<string> result = null)
        {
            string? token = Guid.NewGuid().ToString();
            Mock<ITokenService> mockService = new();
            mockService.Setup(d => d.GetSampleToken()).Returns(Task.FromResult(token));
            return mockService.Object;
        }



        [TestMethod]
        public async Task List_Supplier()
        {
            // Arrange
            ITokenService tokenService = MockTokenService();

            GetTokenQueryHandler handler = new(tokenService);
            GetTokenQuery query = new();

            //Act            
            GetTokenQueryResponse response = await handler.Handle(query, CancellationToken.None);
             
            //Assert
            string? token =await tokenService.GetSampleToken();
            Assert.AreEqual(token, response.Token);
        }

    }
}
