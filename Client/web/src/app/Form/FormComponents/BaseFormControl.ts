import { Injectable, Input } from "@angular/core";
import { FormControl } from "@angular/forms";
import { FieldConfig } from "../Models/FieldConfig";
import { IFormComponent } from "../Models/IFormComponent";
import { IFormHost } from "../Models/IFormHost";

@Injectable()
export abstract class BaseFormControl implements IFormComponent {
  public Controller: FormControl = new FormControl('');
  @Input() public Config?: FieldConfig;
  @Input() Host?: IFormHost;

  ngAfterViewInit(): void { 
    if (this.Config?.Controller) {
      this.Controller = this.Config.Controller;
    }
    
  }
}
