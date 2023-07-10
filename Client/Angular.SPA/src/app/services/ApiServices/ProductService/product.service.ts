import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';
import { Observable, map, tap } from 'rxjs';
import {  Product } from 'src/app/Models/Products/Product';
import { PagedResult } from 'src/app/Models/PagedResult';
import { HttpResponse } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseApiService<Product> {

  public override get RootURL():string{
    return "api/Product";
  }
   
 

}

