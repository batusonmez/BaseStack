import { CellType } from "./CellType";

export interface CellConfig {
  CellType?: CellType,
  Key: string,
  HeaderName: string,
  HasFilter?: boolean,
  HasSort?: boolean
}
