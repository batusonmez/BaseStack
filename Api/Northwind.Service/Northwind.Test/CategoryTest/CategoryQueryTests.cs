using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.Categories.ListCategories;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Domain.Entities;

namespace Northwind.Test.CategoryTest
{
    [TestClass]
    public class CategoryQueryTests : BaseTest
    {
        [TestMethod]
        public async Task List_Category()
        {
            // Arrange
            var uow = InitUOW();
            var mapper = InitNorthwindAPIMapper();
            var repository = new EFRepository<Category>(uow);
            
            var testData = new Category()
            {
                CategoryId = 1,
                CategoryName = "Category 1",
                Description = "Category 1 Description",
                Picture=new byte[] { 1, 2, 3 }
            };

            var testData2 = new Category()
            {
                CategoryId = 2,
                CategoryName = "Category 2",
                Description = "Category 2 Description",
                Picture = new byte[] { 1, 2, 3 }
            };
            DB.Categories.Add(testData);
            DB.Categories.Add(testData2);
            DB.SaveChanges();
            ListCategoriesQueryHandler handler = new ListCategoriesQueryHandler(mapper, repository);
            Query<CategoryDTO> query = new Query<CategoryDTO>();

            //Act            
            QueryResponse<CategoryDTO> response = await handler.Handle(query, CancellationToken.None);



            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.IsTrue(response.Total == 2);
            foreach (var item in DB.Categories)
            {
                CategoryDTO categoryDto = response.FirstOrDefault(d => d.CategoryId == item.CategoryId);

                Assert.IsNotNull(categoryDto);
                Assert.IsTrue(categoryDto.CategoryName == item.CategoryName);
                Assert.IsTrue(categoryDto.Description == item.Description);
                
            }

        }
         
    }
}
