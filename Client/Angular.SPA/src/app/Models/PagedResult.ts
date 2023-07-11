import { PagerConfig } from "../DataTable/Models/PagerConfig";

export class PagedResult<T>{
    Page?:number;
    DataCount?:number;
    TotalPages?:number;
    PageSize:number=0;
    PagerConfig?:PagerConfig;
    Data:T[]=[];
}