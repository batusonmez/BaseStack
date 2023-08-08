import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';

import { Supplier } from 'src/app/Models/Supplier/Supplier.ts';


@Injectable({
  providedIn: 'root'
})
export class SupplierService extends BaseApiService<Supplier> {

  public override get RootURL():string{
    return "api/Supplier";
  } 

}

