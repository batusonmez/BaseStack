import { DataListOption } from "../data-list/datalist.options";

 
export class SelectInputListConfig {

    public Label: string; 
    public Options: DataListOption[]; 

    constructor(label: string, options:DataListOption[]) {
        this.Label = label;      
        this.Options = options;
                
    }

}