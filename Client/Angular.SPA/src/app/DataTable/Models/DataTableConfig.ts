import { CellConfig } from "./CellConfig";
import { CommandConfig } from "./CommandConfig";

export interface DataTableConfig {
  Class?: string,
  Data:any[],
  Cells: CellConfig[],
  Commands:CommandConfig[]
}
