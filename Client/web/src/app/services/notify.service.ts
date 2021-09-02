 
import { Injectable } from '@angular/core';  
declare var toastr: any;
@Injectable({
  providedIn: 'root'
})
export class NotifyService {
  
  constructor() {
    
  }

  Confirm(title: string, callBack: Function) {
    if (confirm(title)) {
      callBack();
    }    
  }

  Notify(title: string) {
    toastr.success(title);
  }

  Error(title: string) {
    toastr.error(title);
  }
   
}
