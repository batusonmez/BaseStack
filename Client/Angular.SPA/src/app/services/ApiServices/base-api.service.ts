import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { Environment } from 'src/environments/environment';
import { LoadingService } from './loading.service';
import { ToastService } from '../ToastService/toast.service';
import { PagedResult } from 'src/app/Models/PagedResult';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService<T> {
  
  constructor(public router: Router, public route: ActivatedRoute, public http: HttpClient,private loadingService:LoadingService,private toastService:ToastService) {

  }

  public Get<U>(path: string): Observable<HttpResponse<U>> {    
    this.addWork(); 
    return this.http.get<U>(Environment.APIRoot + path,{observe:"response"} ).pipe(tap(next=> {      
       this.success(false);   
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

  private success(notify:boolean=true):void{
    if(notify){
      this.toastService.Success("Done!");
    }    
    this.doneWork();
  }

  private error(err:any):void{
    
    this.toastService.Error(err.message);
    this.doneWork();
  }

  public get RootURL():string{
    return ""
  }

  public GetPaged(query:string=""): Observable<PagedResult<T>> {    
    return  this.Get<any>(this.RootURL+"?"+query).pipe(map((resp) =>{ 
     let response=new  PagedResult<T>();
       response.Data=resp.body as T[];
       response.PagerConfig={
        PageSize:Number.parseInt(resp.headers.get('X-Page-Size')??'0'),
        DataCount:Number.parseInt(resp.headers.get('X-Total-Count')??'0'),
        Page: Math.max(Number.parseInt(resp.headers.get('X-Current-Page')??'1'),1),
        TotalPages:Number.parseInt(resp.headers.get('X-Total-Pages')??'0')
       }       
       return response;
     }));      
   };
 
}
