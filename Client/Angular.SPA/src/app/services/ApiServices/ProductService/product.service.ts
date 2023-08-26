import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';
import {  Product } from 'src/app/Models/Products/Product';
import { Observable, map } from 'rxjs';
import { HttpResponse } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseApiService<Product> {

  public override get RootURL():string{    
    return "api/Product";
  }
   
  public override PostLoad(result: Product[]): Product[] {
    result.forEach(d=>
      d.EditLink={
        Label:"Edit",
        QueryParams:{
            Edit:d.ProductId 
        }        
      });

      return result;
  }
  
  public GetProduct(ID:string): Observable<Product> {    
    return super.Get<Product>(this.RootURL+"/ID/"+ID,false).pipe(map((resp) =>{ 
      return resp.body as Product;        
    }));
  }

  public override Post<U>(Data: any): Observable<HttpResponse<U>> {
    debugger
    if(!Data.ProductId){
      delete Data.ProductId;
    }
   return super.Post(Data);
  }

}

