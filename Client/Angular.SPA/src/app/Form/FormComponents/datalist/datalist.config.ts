import { DataListOption } from "./datalist.options";

export class DataListConfig{
    public Label?:string;
    public Options:DataListOption[]=[];
    public Selection?:DataListOption;
    constructor(label:string){
        this.Label=label;
    }
}