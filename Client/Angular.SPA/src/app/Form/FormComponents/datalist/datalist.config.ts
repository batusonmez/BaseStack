import { DataListOption } from "./datalist.options";

export class DataListConfig{
    public Label?:string;
    public Options:DataListOption[]=[]
    constructor(label:string){
        this.Label=label;
    }
}