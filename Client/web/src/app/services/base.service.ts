
import { HttpClient, HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { NotifyService } from './notify.service';
declare var $: any;

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  public root: string;
  constructor(private httpClient: HttpClient, private notifyService: NotifyService) {
    this.root = environment.ApiDomain;
  }

  Get(url: string, notifySuccess: boolean=false): Promise<any> {
    return this.ToPromise(this.httpClient.get(this.root + url), notifySuccess);
  }

  Delete(url: string, notifySuccess: boolean = true): Promise<any> {
    return this.ToPromise(this.httpClient.delete(this.root + url), notifySuccess);
  }

  Post(url: string, payload: any, notifySuccess: boolean = true): Promise<any> {
    return this.ToPromise(this.httpClient.post(this.root + url, payload), notifySuccess);
  }

  ToPromise(source: Observable<any>, notifySuccess: boolean=false): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      $(".loader").show();
      source.toPromise()
        .then(
          res => {
            resolve(res);
            if (notifySuccess) {
              this.notifyService.Notify("Success");
            }
          }
        ).catch(err => {
          this.notifyService.Error("Failed!<br>" + err.message);
        }).finally(() => {
          $(".loader").hide();
        });
    });
    return promise;
  }


}
