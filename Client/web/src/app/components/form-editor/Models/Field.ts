import { SafeHtml } from '@angular/platform-browser';

export class Field {

  public Key: string;
  public Label: string;
  public ControlType: string
  public Required: boolean;
  public Value: any; 
  constructor(Key: string, Label: string, ControlType: string, Required: boolean=true, Value: any = null) {
    this.Key = Key;
    this.Label = Label;
    this.ControlType = ControlType;
    this.Required = Required;
    this.Value = Value;

  } 

}
