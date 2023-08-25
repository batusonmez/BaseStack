import { DataListOption } from "./datalist.options";

export class DataListConfig{
    public Title:string;    
    public LabelFor:string;
    public Options:DataListOption[]=[];
    public Selection?:DataListOption;
    constructor(title:string,labelFor:string){
        this.Title=title;
        this.LabelFor=labelFor;
    }
}