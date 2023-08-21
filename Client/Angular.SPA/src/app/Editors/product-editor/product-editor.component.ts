import { Component, OnInit  } from '@angular/core';
import { CommonModule } from '@angular/common';
 
import { TableHostComponent } from '../../DataTable/table-host/table-host.component';
import { FormHostComponent } from '../../Form/form-host/form-host.component'; 
import { TextInputComponent } from '../../Form/FormComponents/text-input/text-input.component';
import { NumberInputComponent } from '../../Form/FormComponents/number-input/number-input.component';
import { SwitchInputComponent } from '../../Form/FormComponents/switch-input/switch-input.component';
import { SubmitButtonComponent } from '../../Form/FormComponents/submit-button/submit-button.component';
import { IFormHost } from '../../Form/Models/IFormHost';
import { ActivatedRoute } from '@angular/router';
import { RouteMapperService } from '../../services/RouteMapper/route-mapper.service';
import { ProductEditorConfig } from './product-editor.config';
import { ProductService } from '../../services/ApiServices/ProductService/product.service';
import { DataListComponent } from '../../Form/FormComponents/data-list/datalist.component';
import { DataListConfig } from '../../Form/FormComponents/data-list/datalist.config';
import { CategoryService } from '../../services/ApiServices/CategoryService/category.service';
import { SupplierService } from '../../services/ApiServices/SupplierService/supplier.service';
import { DataListOption } from '../../Form/FormComponents/data-list/datalist.options';
import { NumberInputConfig } from '../../Form/FormComponents/number-input/number-input.config';
import { CellTemplates } from '../../DataTable/templates/CellTemplates';
import { BaseEditor } from '../base.editor';
import { CellType } from 'src/app/DataTable/Models/CellType';

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
export class ProductEditorComponent extends BaseEditor implements OnInit {

  Config: ProductEditorConfig = {
    FormConfig: {
      Title:$localize `Product Editor`,
      Name: "TestForm",
      Fields: [
        {
          Name: "ProductName",
          Component: TextInputComponent,
          ComponentData: {
            Label: $localize `Product Name`,
            Placeholder: $localize `Brand name of product`
          }
        },
        {
          Name: "CategoryId",
          Component: DataListComponent,
          Required:true,
          ComponentData: new DataListConfig("Category Name"),
          Event: (eventType: string, param: any) => {
            switch (eventType) {
              case "query":
                let cd = <DataListConfig>this.Config.FormConfig.Fields.find(d => d.Name == "CategoryId")?.ComponentData;
                this.categoryService.GetPaged("keyword=" + param, true).subscribe((rs) => {
                  cd.Options = rs.Data.map((d) => { return { Label: d.CategoryName, Value: d.CategoryId + "", Info: d.Description } });
                  if (!rs.Data.length) {
                    cd.Options = [{ Label: "", Value: "", Info:  $localize `No Data Found` }]
                  }
                });
                break;
                case "optionSelect":
                  debugger
                  console.log(param);
                break;
            }

          }
        }, 
        {
          Name: "SupplierId",
          Component: DataListComponent,
          ComponentData: new DataListConfig("Supplier Name"),
          Required:true,
          Event: (eventType: string, param: any) => {
            switch (eventType) {
              case "query":
                let cd = <DataListConfig>this.Config.FormConfig.Fields.find(d => d.Name == "SupplierId")?.ComponentData;
                this.supplierService.GetPaged("keyword=" + param, true).subscribe((rs) => {
                  cd.Options = rs.Data.map((d) => { return { Label: d.CompanyName, Value: d.SupplierId + "", Info: d.ContactTitle } });
                  if (!rs.Data.length) {
                    cd.Options = [{ Label: "", Value: "", Info: $localize `No Data Found` }]
                  }
                });
                break;
                case "optionSelect":
                  console.log(param);
                break;
            }
          }
        }, 
        {
          Name: "QuantityPerUnit",
          Component: TextInputComponent,
          ComponentData: {
            Label: $localize `Quantity Per Unit`,
            Placeholder: $localize `eg: 10pic, 1LT`
          }
        },
        {
          Name: "UnitPrice",
          Component: NumberInputComponent,
          ComponentData: new NumberInputConfig($localize `Unit Price`,10000,0, $localize `Cost per unit`) 
        },
        {
          Name: "UnitsInStock",
          Component: NumberInputComponent,
          ComponentData: new NumberInputConfig($localize `Units in Stock`,1000,0, $localize `Available stocks to ship`) 
        },
        {
          Name: "UnitsOnOrder",
          Component: NumberInputComponent,
          ComponentData: new NumberInputConfig($localize `Units on Order`,1000,0,$localize `Units waiting to deliver`) 
        },
        {
          Name: "ReorderLevel",
          Component: NumberInputComponent,
          ComponentData: new NumberInputConfig($localize `Reorder Level`,1000,0, $localize `Units waiting to reorder`) 
        },
        {
          Name: "Discontinued",
          Component: SwitchInputComponent,
          ComponentData:  {
            Label: "Discontinued" 
          },
          
        },
        {
          Name: "CompSb",
          Component: SubmitButtonComponent,
          ComponentData: {
            Label: "Submit",
            CancelLabel: "Cancel"
          },          
          Event: (eventType?: string, param?: any) => {
            debugger
            switch (eventType) {
              case "Cancel":
                this.mapper.setQueryParams({ editor: 0 });
                break;
            }
          }
        }
      ],
      FormEvent: (eventType: string, FormData?: IFormHost, param?: any) => {
        switch (eventType) {
          case "Submit": 
             this.productService.Post(FormData?.Form.value).subscribe(res=>{
                
             });
            break;
        }
      }
    },
    DataTableConfig: {
      Cells: [
        {           
          Binder: "ProductName",
          HeaderName: $localize `Product Name`
        },
        {           
          Binder: "CategoryName",
          HeaderName:  $localize `Category Name`
        },
        {           
          Binder: "SupplierName",
          HeaderName:$localize `Supplier Name`
        },
        {           
          Binder: "EditLink",
          HeaderName:"",
          CellType:CellType.Link
        } 
      ],
      Commands: [
        {
          Title: $localize `New`,
          Class: "btn btn-success",
          Command: { editor: 1 }
        }
      ],
      Data: [],
      Pager: {
        PageSize: 10
      },
      OnFilter: (filter: string) => {
        this.LoadData(filter);
      }
    },
    ShowEditor: false

  }

  constructor(private route: ActivatedRoute, private mapper: RouteMapperService, private productService: ProductService, private categoryService: CategoryService, private supplierService: SupplierService) {
    super();
   }
   
  ngOnInit(): void {
    this.registerQueryCommands();
  }

  LoadData(query: string): void {
 
    this.productService.GetPaged(query).subscribe(res => {
 debugger
      this.Config.DataTableConfig.Data = res.Data;
      if (res.PagerConfig) {
        this.Config.DataTableConfig.Pager = res.PagerConfig
      } 
    })
  }

  registerQueryCommands(): void {
    this.mapper.registerQueryParams(this.route, [
      {
        parameter: "editor",
        action: (prm: string) => {
          if (prm == "1") { 
            this.ToggleEditor(true);
          } else {
            this.ToggleEditor(false);
          } 
        }
      }
    ])
  }
 

  ToggleEditor(show: boolean): void {
    this.Config.ShowEditor = show;
  }

}
