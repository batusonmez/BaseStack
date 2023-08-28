using Authentication.Application.Commands.Clients.CreateClient;
namespace Authentication.Test.Clients
{
    [TestClass]
    public class CreateClientCommandTests : BaseTest
    {

 

        [TestInitialize]
        public void Setup()
        {
            ResetTestDB();
       
        }

        [TestMethod]
        public async Task Create_Client_Command()
        {
            //// Arrange   
            //CreateClientCommand testCommand = new ( );
            //CreateClientCommandHandler handler = new ();

            ////Act 
            //CreateClientCommandResponse response = await handler.Handle(testCommand, CancellationToken.None);

            ////Assert
            //ClearTestConnection(); 
            //Assert.IsNotNull(response);

        }
         
    }
}
