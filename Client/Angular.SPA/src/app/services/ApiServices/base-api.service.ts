import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { Environment } from 'src/environments/environment';
import { LoadingService } from './loading.service';
import { ToastService } from '../ToastService/toast.service';
import { PagedResult } from 'src/app/Models/PagedResult';
import { AuthenticationService } from './AuthenticationService/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService<T> {
  
  constructor(public router: Router, 
     public route: ActivatedRoute,
     public http: HttpClient,
     private loadingService:LoadingService,
     private toastService:ToastService,
     private authService:AuthenticationService
     ) {

  }

  public Get<U>(path: string,hidebackdrop:boolean=false): Observable<HttpResponse<U>> {    
    if(!hidebackdrop){
      this.addWork();     
    }    
    let defaultHeaders=this.GetDefaultHeaders();
    return this.http.get<U>(Environment.APIRoot + path,{observe:"response",headers:defaultHeaders} ).pipe(tap(next=> {      
       this.success(false,hidebackdrop);   
    }), catchError((err)=>{      
      this.error(err)
      return of();
    }));
  }
  
  public Delete(path: string,hidebackdrop:boolean=false): Observable<HttpResponse<any>> {    
    if(!hidebackdrop){
      this.addWork();     
    }    
    let defaultHeaders=this.GetDefaultHeaders();
    return this.http.delete(Environment.APIRoot + path,{observe:"response",headers:defaultHeaders} ).pipe(tap(next=> {      
       this.success(false,hidebackdrop);   
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

  private success(notify:boolean=true,hideBackDrop:boolean=false):void{
    if(notify){
      this.toastService.Success("Done!");
    }    
    if(!hideBackDrop){
      this.doneWork();
    }    
  }


  public GetDefaultHeaders():HttpHeaders{
    
    let headers:any={};
    let token=this.authService.GetToken();
    if(token){
      headers['Authorization']="Bearer "+token;
    }
    
    return new HttpHeaders(headers);
  
  }

  private error(err:any):void{
    
    this.toastService.Error(err.message);
    this.doneWork();
  }

  public get RootURL():string{
    return ""
  }

  public GetPaged(query:string="",hideBackdrop:boolean=false): Observable<PagedResult<T>> {    
    return  this.Get<any>(this.RootURL+"?"+query,hideBackdrop).pipe(map((resp) =>{ 
     let response=new  PagedResult<T>();
       response.Data=this.PostLoad( resp.body as T[]);
       response.PagerConfig={
        PageSize:Number.parseInt(resp.headers.get('X-Page-Size')??'0'),
        DataCount:Number.parseInt(resp.headers.get('X-Total-Count')??'0'),
        Page: Math.max(Number.parseInt(resp.headers.get('X-Current-Page')??'1'),1),
        TotalPages:Number.parseInt(resp.headers.get('X-Total-Pages')??'0')
       }       
      
       return response;
     }));      
   };

   public PostLoad(result:T[]):T[]{
      return result;
   }

   public GetByID(ID:string): Observable<T> {    
    return this.Get<T>(this.RootURL+"/ID/"+ID,false).pipe(map((resp) =>{ 
      return resp.body as T;        
    }));
   }
   
   public DeleteByID(ID:string): Observable<HttpResponse<any>> {    
    return this.Delete(this.RootURL+"/ID/"+ID,false);
   }

   public Post<U>(Data:any): Observable<HttpResponse<U>> {        
    this.addWork();             
    let defaultHeaders=this.GetDefaultHeaders();
    return this.http.post<U>(Environment.APIRoot+this.RootURL,Data,{observe:"response",headers:defaultHeaders} ).pipe(tap(next=> {      
       this.success(true);   
    }), catchError((err)=>{      
      this.error(err)
      return of();
    })); 
  }
 
}
