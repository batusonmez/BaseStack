import { CellLink } from "./CellLink";
import { CellType } from "./CellType";

 
export interface CellConfig { 
  Binder: any,
  HeaderName: string,
  CellType?:CellType,
  HasFilter?: boolean,
  HasSort?: boolean
}
