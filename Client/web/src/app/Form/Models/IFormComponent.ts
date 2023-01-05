import { FormControl } from "@angular/forms";
import { FieldConfig } from "./FieldConfig";
import { IFormHost } from "./IFormHost";

export interface IFormComponent {
  Config?: FieldConfig,
  Controller: FormControl,
  Host?: IFormHost
}
