﻿using Dispatcher;
using Northwind.Application.Models.DTO;

namespace Northwind.API.Controllers.Products
{

    public class ProductController : BaseAPIController<ProductsDTO>
    {        
        public ProductController(IDispatcher dispatcher
            ):base(dispatcher)
        {            
        }
         

    }
}
