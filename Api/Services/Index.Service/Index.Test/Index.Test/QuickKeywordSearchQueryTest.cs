using Index.Application.Common;
using Index.Application.Queries.ListForKeys;
using Index.Application.Queries.QuickKeywordSearchQuery;
using Moq;

namespace Index.Test
{
    [TestClass]
    public class QuickKeywordSearchQueryTest
    {
        [TestMethod]
        public async Task Search_Matched_Query()
        {
            //Arrange
            Mock<IIndexer>  indexer = new();
            IEnumerable<string> mockResult = new string[] { "1", "2", "3" };
            indexer.Setup(d => d.QuickKeywordSearch(It.IsAny<QuickKeywordSearchRequest>())).Returns(mockResult);
            QuickKeywordSearchHandler handler = new(indexer.Object);
            QuickKeywordSearchRequest query = new QuickKeywordSearchRequest();

            //Act
            QuickKeywordSearchResponse response= await handler.Handle(query,CancellationToken.None);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() == mockResult.Count());
            foreach (var item in mockResult)
            {
                Assert.IsTrue(response.Contains(item));
            }

        }
    }
}