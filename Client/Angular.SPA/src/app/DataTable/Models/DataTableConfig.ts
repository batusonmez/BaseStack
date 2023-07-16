import { CellConfig } from "./CellConfig";
import { CommandConfig } from "./CommandConfig";
import { PagerConfig } from "./PagerConfig";


export interface DataTableConfig {
  Class?: string,
  Data:any[],
  Cells: CellConfig[],
  Commands:CommandConfig[],
  Pager?:PagerConfig ,
  OnFilter(filter: string): void 
}
