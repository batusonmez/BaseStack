import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { ToastMessage } from "./toast.message";

@Injectable({
    providedIn: 'root'
})
export class ToastService {
 
    private subject=new Subject<ToastMessage>();

   public   Success(message:string):void{
        this.publish({Type:"Success",Message:message});
    }

    public   Warning(message:string):void{
        this.publish({Type:"Warning",Message:message});
    }

    public   Error(message:string):void{
        this.publish({Type:"Error",Message:message});
    }

    private publish(data:ToastMessage):void{
        this.subject.next(data)
    }

    public OnToast():Observable<ToastMessage>{
        return this.subject.asObservable();
    }

}
