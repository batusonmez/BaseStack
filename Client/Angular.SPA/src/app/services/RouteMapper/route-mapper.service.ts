import { Injectable } from '@angular/core';
import { ActivatedRoute, QueryParamsHandling, Router } from '@angular/router';
import { RouteMap } from './RouteMap';

@Injectable({
  providedIn: 'root'
})
export class RouteMapperService { 
  constructor(private router:Router,private route:ActivatedRoute) {
    
   }

  registerQueryParams(route:ActivatedRoute,map:RouteMap[] ):void{   
    route.queryParams
    .subscribe(params => {
      map.forEach(m => {
        let p=params[m.parameter];
        if(p){
          m.action(p);
         } 
      });                 
    });
  }

  setQueryParams(queryParams:any,handler:QueryParamsHandling='merge'  ):void{
    this.router.navigate([], { queryParams, relativeTo: this.route, queryParamsHandling: handler });
  }
  
}
