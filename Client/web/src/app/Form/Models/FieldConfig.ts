import { Type } from "@angular/core"; 
import { FormControl } from "@angular/forms";
import { IFormComponent } from "./IFormComponent";


export interface FieldConfig {
  Name: string,
  Component: Type<IFormComponent>,  
  Value?: any,
  ComponentData?: any,
  Controller?: FormControl
}
