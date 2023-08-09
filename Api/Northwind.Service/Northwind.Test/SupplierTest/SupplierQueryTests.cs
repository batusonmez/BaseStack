using AutoMapper;
using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.Suppliers.ListSuppliers;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Test.SupplierTest
{
    [TestClass]
    public class SupllierQueryTests : BaseTest
    {
        [TestMethod]
        public async Task List_Supplier()
        {
            // Arrange
            ResetTestDB();
            IUOW uow = InitUOW();
            IMapper mapper = InitNorthwindAPIMapper();
            EFRepository<Category> repository = new(uow);
            IIndexService indexService = MockIndexService();
            Supplier testData = new()
            {

             SupplierId =1,
             CompanyName ="Company 1",
             ContactName =" Contact 1",
             ContactTitle ="Title 1",
             Address="Adress 1",
             City = "City 1",
             Region= "Region 1",
             PostalCode ="Postal code 1",
             Country= "Country 1",
             Phone ="Phone 1",
             Fax="Fax 1",
             HomePage ="https://www.company1.com"
            };

            Supplier testData2 = new()
            {

             SupplierId =2,
             CompanyName ="Company 2",
             ContactName =" Contact 2",
             ContactTitle ="Title 2",
             Address="Adress 2",
             City = "City 2",
             Region= "Region 1",
             PostalCode ="Postal code 2",
             Country= "Country 1",
             Phone ="Phone 2",
             Fax="Fax 2",
             HomePage ="https://www.company2.com"
            };
            DB.Supplier.Add(testData);
            DB.Supplier.Add(testData2);
            DB.SaveChanges();

            ListSupplierQueryHandler handler = new (mapper, repository, indexService);
            Query<SupplierDTO> query = new ();

            //Act            
            QueryResponse<SupplierDTO> response = await handler.Handle(query, CancellationToken.None);



            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.IsTrue(response.Total == 2);
            foreach (var item in DB.Supplier)
            {
                SupplierDTO supplierDto = response.FirstOrDefault(d => d.SupplierId == item.SupplierId);

                Assert.IsNotNull(supplierDto);
                Assert.IsTrue(supplierDto.CompanyName == item.CompanyName);
                Assert.IsTrue(supplierDto.ContactName == item.ContactName);
                Assert.IsTrue(supplierDto.ContactTitle == item.ContactTitle);
                Assert.IsTrue(supplierDto.Address == item.Address);
                Assert.IsTrue(supplierDto.City == item.City);
                Assert.IsTrue(supplierDto.Region == item.Region);
                Assert.IsTrue(supplierDto.Phone == item.Phone);
                Assert.IsTrue(supplierDto.Fax == item.Fax);
                Assert.IsTrue(supplierDto.HomePage == item.HomePage);

            }
        }

        [TestMethod]
        public async Task List_Supplier_Quick_Search()
        {
            // Arrange
            ResetTestDB();
            IUOW uow = InitUOW();
            IMapper mapper = InitNorthwindAPIMapper();
            EFRepository<Supplier> repository = new(uow);
            IIndexService indexService = MockIndexService(new string[] {"1","3"});
            Supplier testData = new()
            {

             SupplierId =1,
             CompanyName ="Company 1",
             ContactName =" Contact 1",
             ContactTitle ="Title 1",
             Address="Adress 1",
             City = "City 1",
             Region= "Region 1",
             PostalCode ="Postal code 1",
             Country= "Country 1",
             Phone ="Phone 1",
             Fax="Fax 1",
             HomePage ="https://www.company1.com"
            };

            Supplier testData2 = new()
            {

             SupplierId =2,
             CompanyName ="Company 2",
             ContactName =" Contact 2",
             ContactTitle ="Title 2",
             Address="Adress 2",
             City = "City 2",
             Region= "Region 1",
             PostalCode ="Postal code 2",
             Country= "Country 1",
             Phone ="Phone 2",
             Fax="Fax 2",
             HomePage ="https://www.company2.com"
            };
            Supplier testData3 = new()
            {

             SupplierId =3,
             CompanyName ="Company 3",
             ContactName =" Contact 3",
             ContactTitle ="Title 3",
             Address="Adress 3",
             City = "City 2",
             Region= "Region 1",
             PostalCode ="Postal code 2",
             Country= "Country 1",
             Phone ="Phone 3",
             Fax="Fax 3",
             HomePage ="https://www.company3.com"
            };
            DB.Supplier.Add(testData);
            DB.Supplier.Add(testData2);
            DB.Supplier.Add(testData3);
            DB.SaveChanges();
            ListSupplierQueryHandler handler = new (mapper, repository, indexService);
            Query<SupplierDTO> query = new();
            query.QuickSearchKeyword = "temp";

            //Act            
            QueryResponse<SupplierDTO> response = await handler.Handle(query, CancellationToken.None);


            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.IsTrue(response.Total == 2);
            foreach (var item in DB.Supplier.Where(d=> d.SupplierId ==1 || d.SupplierId == 3))
            {
                SupplierDTO supplierDTO = response.FirstOrDefault(d => d.SupplierId == item.SupplierId);

                Assert.IsNotNull(categoryDto);
                Assert.IsTrue(supplierDTO.CompanyName == item.CompanyName);
                Assert.IsTrue(supplierDTO.ContactName == item.ContactName);
                Assert.IsTrue(supplierDTO.ContactTitle == item.ContactTitle);
                Assert.IsTrue(supplierDTO.Address == item.Address);
                Assert.IsTrue(supplierDTO.City == item.City);
                Assert.IsTrue(supplierDTO.Region == item.Region);
                Assert.IsTrue(supplierDTO.Phone == item.Phone);
                Assert.IsTrue(supplierDTO.Fax == item.Fax);
                Assert.IsTrue(supplierDTO.HomePage == item.HomePage);

            }
        }
    }
}
