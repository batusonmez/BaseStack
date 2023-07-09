export interface Product {
    ProductId: number;
    ProductName?: string;
    SupplierId?: number;
    CategoryId?: number;
    QuantityPerUnit?: string;
    UnitPrice?: number;
    UnitsInStock?: number;
    UnitsOnOrder?: number;
    ReorderLevel?: number;
    Discontinued: boolean;
    CategoryName?: string;
    SupplierName?: string;
}