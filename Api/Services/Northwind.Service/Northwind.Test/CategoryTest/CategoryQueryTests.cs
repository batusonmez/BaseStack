using AutoMapper;
using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.Categories.ListCategories;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Test.CategoryTest
{
    [TestClass]
    public class CategoryQueryTests : BaseTest
    {
        [TestMethod]
        public async Task List_Category()
        {
            // Arrange
            ResetTestDB();
            IUOW uow = InitUOW();
            IMapper mapper = InitNorthwindAPIMapper();
            EFRepository<Category> repository = new(uow);
            IIndexService indexService = MockIndexService();
            Category testData = new()
            {
                CategoryId = 1,
                CategoryName = "Category 1",
                Description = "Category 1 Description",
                Picture = new byte[] { 1, 2, 3 }
            };

            Category testData2 = new()
            {
                CategoryId = 2,
                CategoryName = "Category 2",
                Description = "Category 2 Description",
                Picture = new byte[] { 1, 2, 3 }
            };
            DB.Categories.Add(testData);
            DB.Categories.Add(testData2);
            DB.SaveChanges();
            ListCategoriesQueryHandler handler = new ListCategoriesQueryHandler(mapper, repository, indexService);
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

        [TestMethod]
        public async Task List_Category_Quick_Search()
        {
            // Arrange
            ResetTestDB();
            IUOW uow = InitUOW();
            IMapper mapper = InitNorthwindAPIMapper();
            EFRepository<Category> repository = new(uow);
            IIndexService indexService = MockIndexService(new string[] {"1","3"});
            Category testData = new()
            {
                CategoryId = 1,
                CategoryName = "Category 1",
                Description = "Category 1 Description",
                Picture = new byte[] { 1, 2, 3 }
            };

            Category testData2 = new()
            {
                CategoryId = 2,
                CategoryName = "Category 2",
                Description = "Category 2 Description",
                Picture = new byte[] { 1, 2, 3 }
            };

            Category testData3 = new()
            {
                CategoryId = 3,
                CategoryName = "Category 3",
                Description = "Category 3 Description",
                Picture = new byte[] { 1, 2, 3 }
            };
            DB.Categories.Add(testData);
            DB.Categories.Add(testData2);
            DB.Categories.Add(testData3);
            DB.SaveChanges();
            ListCategoriesQueryHandler handler = new ListCategoriesQueryHandler(mapper, repository, indexService);
            Query<CategoryDTO> query = new Query<CategoryDTO>();
            query.QuickSearchKeyword = "temp";

            //Act            
            QueryResponse<CategoryDTO> response = await handler.Handle(query, CancellationToken.None);


            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.IsTrue(response.Total == 2);
            foreach (var item in DB.Categories.Where(d=> d.CategoryId==1 || d.CategoryId == 3))
            {
                CategoryDTO categoryDto = response.FirstOrDefault(d => d.CategoryId == item.CategoryId);

                Assert.IsNotNull(categoryDto);
                Assert.IsTrue(categoryDto.CategoryName == item.CategoryName);
                Assert.IsTrue(categoryDto.Description == item.Description);

            }
        }
    }
}
