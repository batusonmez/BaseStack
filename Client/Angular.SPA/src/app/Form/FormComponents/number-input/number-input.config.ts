export class NumberInputConfig{
    public Label:string; 
    public Max?:number;
    public Min?:number;
    public Placeholder?:string

    constructor(label:string, max?:number,min?:number,placeholder?:string ){
        this.Label=label; 
        this.Max=max;
        this.Min=min;
        this.Placeholder=placeholder;    
    }
}