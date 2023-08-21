import { CellLink } from "src/app/DataTable/Models/CellLink";

export interface Product {
    ProductId: number ;
    ProductName?: string;
    SupplierId?: number;
    CategoryId?: number;
    QuantityPerUnit?: string;
    UnitPrice?: number;
    UnitsInStock?: number;
    UnitsOnOrder?: number;
    ReorderLevel?: number;
    Discontinued: boolean ;
    CategoryName?: string;
    SupplierName?: string; 
    EditLink?:CellLink;    
  
}