import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableHostComponent } from '../DataTable/table-host/table-host.component';
import { FormHostComponent } from '../Form/form-host/form-host.component';
import { CellType } from '../DataTable/Models/CellType';
import { SubmitButtonComponent } from '../Form/FormComponents/submit-button/submit-button.component';
import { TextInputComponent } from '../Form/FormComponents/text-input/text-input.component';
import { IFormHost } from '../Form/Models/IFormHost';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteMapperService } from '../services/RouteMapper/route-mapper.service';
import { ProductEditorConfig } from './product-editor.config';
import { ProductService } from '../services/ApiServices/ProductService/product.service'; 
import { DataListComponent } from '../Form/FormComponents/datalist/datalist.component';
import { DataListConfig } from '../Form/FormComponents/datalist/datalist.config';
import { CategoryService } from '../services/ApiServices/CategoryService/category.service';
import { DataListOption } from '../Form/FormComponents/datalist/datalist.options';

@Component({
  selector: 'product-editor',
  standalone: true,
  imports: [
    CommonModule,
    FormHostComponent,
    TableHostComponent
  ],
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.scss']
})
export class ProductEditorComponent implements OnInit {
   
  Config: ProductEditorConfig = {
    FormConfig: {
      Name: "TestForm",
      Fields: [
        {
          Name: "CategoryId",
          Component: DataListComponent,
          ComponentData:new DataListConfig("Category Name"),
          Event:(eventType:string,param:any )=>{            
              let cd= <DataListConfig>this.Config.FormConfig.Fields.find(d=>d.Name=="CategoryId")?.ComponentData;              
              this.categoryService.GetPaged("keyword="+param,true).subscribe((rs)=>{                
                cd.Options=rs.Data.map((d)=>{return {Label: d.CategoryName,Value:d.CategoryId+"",Info:d.Description}});             
              });
              
          }
        },
        {
          Name: "Comp2",
          Component: TextInputComponent,
          ComponentData: {
            Label: "Label comp2",
            Placeholder: "A place holder"
          }
        },
        {
          Name: "Comp3",
          Component: TextInputComponent,
          ComponentData: {
            Label: "Label comp3"
          }
        },
        {
          Name: "CompSb",
          Component: SubmitButtonComponent,
          ComponentData: {
            Label: "Submit",
            CancelLabel: "Cancel"
          },
          Event: (eventType?: string, param?: any) => {
            switch (eventType) {
              case "Cancel":
                this.mapper.setQueryParams({ editor: 0 });
                break;
            }
          }
        }
      ],
      FormEvent: (eventType: string, FormData?: IFormHost, param?: any) => {

      }
    },
    DataTableConfig: {
      Cells: [
        {
          CellType: CellType.Cell,
          Key: "ProductName",
          HeaderName: "Product Name"
        },
        {
          CellType: CellType.Cell,
          Key: "CategoryName",
          HeaderName: "Category Name"
        },
        {
          CellType: CellType.Cell,
          Key: "SupplierName",
          HeaderName: "Supplier Name"
        }
      ],
      Commands: [
        {
          Title: "New",
          Class: "btn btn-success",
          Command: { editor: 1 }
        }
      ],
      Data: [ ],
      Pager:{
        PageSize:10        
      },
      OnFilter:(filter:string)=>{
        this.LoadData(filter);
      }
    },
    ShowEditor: false 
    
  }

  constructor(private route: ActivatedRoute, private mapper: RouteMapperService,private productService:ProductService, private categoryService:CategoryService) { }
  ngOnInit(): void {

    this.registerQueryCommands();

  }

  LoadData(query:string):void{
    this.productService.GetPaged(query).subscribe(res=>{
      this.Config.DataTableConfig.Data=res.Data;
      if(res.PagerConfig){
        this.Config.DataTableConfig.Pager=res.PagerConfig
      }        
  })
  }

  registerQueryCommands(): void {
    this.mapper.registerQueryParams(this.route, [
      {
        parameter: "editor",
        action: (prm: string) => {
          if (prm == "1") {
            this.NewProduct(prm);
            this.ToggleEditor(true);
          } else {
            this.ToggleEditor(false);
          }

        }
      }
    ])
  }

  NewProduct(prm: string): void {


  }

  ToggleEditor(show: boolean): void {
    this.Config.ShowEditor = show;
  }
 
}
