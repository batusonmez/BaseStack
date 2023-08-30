import { Injectable } from '@angular/core';
import { BaseApiService } from '../base-api.service';

import { Observable, map } from 'rxjs';
import { AuthToken } from 'src/app/Models/Authentication/Token';
import { HttpClient } from '@angular/common/http';
import { Environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService     {

  static TOKEN_STORAGE_NAME: string = "token"
  public   get RootURL(): string {
    return Environment.APIRoot +"api/Token";
  }

  constructor( public http: HttpClient){

  }
  
  public SetToken(): Observable<AuthToken> {
    return this.http.get<AuthToken>(this.RootURL,{observe:"response"} ).pipe(map((resp) => {
      
      let authToken = resp.body as AuthToken;
      if (authToken) {
        let JWTToken = JSON.parse(authToken.Token);
        if (JWTToken) {
          localStorage.setItem(AuthenticationService.TOKEN_STORAGE_NAME, JWTToken.access_token.replace(/[\r\n]+/g, ' ') )
        }
      }
      return authToken;
    }));
  }


  public GetToken(): string | null {
    return localStorage.getItem(AuthenticationService.TOKEN_STORAGE_NAME);
  }


  public DeleteToken():void{
    localStorage.removeItem(AuthenticationService.TOKEN_STORAGE_NAME);
  }

}

