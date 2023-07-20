import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';

import { Category } from 'src/app/Models/Categories/Category';


@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseApiService<Category> {

  public override get RootURL():string{
    return "api/Category";
  }
   
 

}

