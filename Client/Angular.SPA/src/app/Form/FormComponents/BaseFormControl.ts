import { Injectable, Input } from "@angular/core";
import { FormControl } from "@angular/forms";
import { FieldConfig } from "../Models/FieldConfig";
import { IFormComponent } from "../Models/IFormComponent";
import { IFormHost } from "../Models/IFormHost";

@Injectable()
export abstract class BaseFormControl implements IFormComponent  {

  public Controller: FormControl = new FormControl('');
  @Input() public Config!: FieldConfig;
  @Input() Host?: IFormHost;

 
  constructor(){
    if (this.Config?.Controller) {
      this.Controller = this.Config.Controller;
       this.RegisterForm();
    }
  }
  

  public RegisterForm(): void {
    if (this.Host?.Form) {
      this.Host.Form.addControl(this.Config.Name,this.Controller);
    }
  }
}
