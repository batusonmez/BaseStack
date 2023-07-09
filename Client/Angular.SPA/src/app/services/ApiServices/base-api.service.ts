import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, catchError, of, tap } from 'rxjs';
import { Environment } from 'src/environments/environment';
import { LoadingService } from './loading.service';
import { ToastService } from '../ToastService/toast.service';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService {
  constructor(public router: Router, public route: ActivatedRoute, public http: HttpClient,private loadingService:LoadingService,private toastService:ToastService) {

  }

  public Get<T>(path: string): Observable<T> {    
    this.addWork();
    return this.http.get<T>(Environment.APIRoot + path ).pipe(tap(next=> {      
       this.success();   
    }), catchError((err)=>{      
      this.error(err)
      return of();
    }));
  }

  private addWork():void{
    this.loadingService.AddWork();    
  }

  private doneWork():void{
    this.loadingService.DoneWork();
  }

  private success():void{
    this.toastService.Success("Done!");
    this.doneWork();
  }

  private error(err:any):void{
    
    this.toastService.Error(err.message);
    this.doneWork();
  }
 
}
