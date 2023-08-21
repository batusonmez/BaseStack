import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';
import {  Product } from 'src/app/Models/Products/Product';


@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseApiService<Product> {

  public override get RootURL():string{    
    return "api/Product";
  }
   
  public override PostLoad(result: Product[]): Product[] {
    result.forEach(d=>d.EditLink={
             Label:"Edit",
             QueryParams:{
                 Edit:d.ProductId 
             }        
         })
         return result;
  }
 

}

