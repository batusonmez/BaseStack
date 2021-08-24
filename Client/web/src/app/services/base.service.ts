
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HttpService {

  public root: string;
  constructor(private httpClient: HttpClient) {
    this.root = environment.ApiDomain;
  }

  Get(url: string): Observable<any>{
    return this.httpClient.get(this.root+url);
  }

  Post(url: string, payload: any): Observable<any> {
    return this.httpClient.post(this.root + url, payload);
  }
}
