import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';
import { Observable, map, tap } from 'rxjs';
import { Person } from 'src/app/Models/Person/Person';


@Injectable({
  providedIn: 'root'
})
export class PersonService extends BaseApiService {

  public GetAllUsers(): Observable<Person[]> {    
   return super.Get<any>("personapi/Person").pipe(map(resp =>{ 
       return resp.PersonList as Person[];
      }))      
  };
}

