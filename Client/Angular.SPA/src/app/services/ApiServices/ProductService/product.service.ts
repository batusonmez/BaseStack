import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';
import { Observable, map, tap } from 'rxjs';
import {  Product } from 'src/app/Models/Products/Product';
import { PagedResult } from 'src/app/Models/PagedResult';


@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseApiService {

  public GetProducts(): Observable<PagedResult<Product>> {    
   return super.Get<any>("api/Product").pipe(map((resp) =>{ 
    let response=new  PagedResult<Product>();
      response.Data=resp as Product[];
      return response;
    }));      
  };
}

