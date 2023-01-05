import { FormGroup } from "@angular/forms";
import { FormConfig } from "./FormConfig";

export interface IFormHost {
  Form: FormGroup,
  Config: FormConfig
}
