 
import { Injectable } from '@angular/core';  
 
@Injectable({
  providedIn: 'root'
})
export class ConfirmationService {
  
  constructor() {
    
  }

  Confirm(title: string, callBack: Function) {
    if (confirm(title)) {
      callBack();
    }
    
  }
   
}
