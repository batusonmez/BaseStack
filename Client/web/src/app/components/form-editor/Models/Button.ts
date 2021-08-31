import { SafeHtml } from '@angular/platform-browser';
import { Field } from './Field';

export class Button extends Field {

  
  public Class: string;
  public ButtonType: string;
  public Action: Function;
  constructor(Label: string, ButtonType: string, Class: string = "btn btn-primary", section: string = "body", Action: Function = () => { }) {
    super("__button", Label, "button", false, null, section);
    this.ButtonType = ButtonType
    this.Class = Class;
    this.Action = Action;
  } 

}
