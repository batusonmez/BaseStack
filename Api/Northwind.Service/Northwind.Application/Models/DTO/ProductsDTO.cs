﻿
using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Models.DTO
{
    public class ProductsDTO : IDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }

        public object IndexKey => ProductId;
        public bool IndexEnabled => true;

        public bool HasID => ProductId > 0;
    }
}