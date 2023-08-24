using MediatR;
using Northwind.Application.Models.DTO;

namespace Northwind.Application.Queries.Products.GetProduct
{
    public class GetProductQuery: IRequest<ProductsDTO>
    {
        public int ProductID { get; set; }
        public GetProductQuery(int productID)
        {
            ProductID = productID;  
        }
    }
}
