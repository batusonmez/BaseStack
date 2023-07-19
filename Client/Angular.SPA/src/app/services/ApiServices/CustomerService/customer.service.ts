import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';

import { Customer } from 'src/app/Models/Customers/Product';


@Injectable({
  providedIn: 'root'
})
export class CustomerService extends BaseApiService<Customer> {

  public override get RootURL():string{
    return "api/Customer";
  }
   
 

}

