import { FieldConfig } from "./FieldConfig";
import { IFormHost } from "./IFormHost";

export interface FormConfig {
  Fields: FieldConfig[],
  Name: string,
  Title:string,
  FormEvent(eventType: string, FormData?: IFormHost, param?: any): void
}
