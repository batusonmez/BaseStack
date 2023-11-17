import { DataListOption } from "./datalist.options";

export class DataListConfig {

    public Title: string;
    public LabelFor: string;
    public Options: DataListOption[];
    public Selection?: DataListOption;
    public SelectionMultiple?: DataListOption[];
    public Multiple: boolean;
    public ValueFor?: string;

    constructor(title: string, labelFor: string, multiple: boolean = false, valueFor: string | undefined = undefined) {
        this.Title = title;
        this.LabelFor = labelFor;
        this.Multiple = multiple;
        this.Options = [];
        this.SelectionMultiple = [];
        this.ValueFor = valueFor;
    }

}