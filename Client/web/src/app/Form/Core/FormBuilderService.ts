import { Injectable } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { FormConfig } from "../Models/FormConfig";

@Injectable({
  providedIn: 'root',
})
export class FormBuilderService {

  public BuildForm(config: FormConfig): FormGroup {
    let controls: any = {};
    for (var i = 0; i < config.Fields.length; i++) {
      let field = config.Fields[i]; 
      controls[field.Name] = new FormControl(field.Value);
    }
    let group = new FormGroup(controls);

    return group;
  }
}
