import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    loadCount: number;
    constructor() {
        this.loadCount = 0
    }

    public AddWork():void{
        this.loadCount++;
    }

    public DoneWork():void{
        this.loadCount--;
        if(this.loadCount<0){
            this.loadCount=0;
        }
    }

   public get Loading():boolean{
        return this.loadCount>0;
    }

}
