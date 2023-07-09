export class PagedResult<T>{
    Page?:number;
    DataCount?:number;
    TotalPages?:number;
    PageSize:number=0;
    Data:T[]=[];
}