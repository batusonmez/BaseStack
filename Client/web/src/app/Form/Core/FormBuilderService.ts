import { Injectable } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms"; 
import { IFormHost } from "../Models/IFormHost";

@Injectable({
  providedIn: 'root',
})
export class FormBuilderService {

  public BuildForm(host: IFormHost): FormGroup {
    let controls: any = {};
    for (var i = 0; i < host.Config.Fields.length; i++) {
      let field = host.Config.Fields[i];       
      if (!field.Controller) {
        field.Controller = new FormControl(field.Value);
      }
      controls[field.Name] = field.Controller;
    }
    let group = new FormGroup(controls);

    return group;
  }



}
