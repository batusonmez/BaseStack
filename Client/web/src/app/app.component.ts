import { Component } from '@angular/core'; 
import { CellType } from './DataTable/Models/CellType';
import { DataTableConfig } from './DataTable/Models/DataTableConfig';
import { SubmitButtonComponent } from './Form/FormComponents/submit-button/submit-button.component';
import { TextInputComponent } from './Form/FormComponents/text-input/text-input.component';
import { FormConfig } from './Form/Models/FormConfig';
import { IFormHost } from './Form/Models/IFormHost';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'web';
  Config: FormConfig = {
    Name:"TestForm",
    Fields: [
      {
        Name: "Comp1",
        Component: TextInputComponent,
        ComponentData: {
          Label:"Label comp1"
        } 
      },
      {
        Name: "Comp2",
        Component: TextInputComponent,
        ComponentData: {
          Label: "Label comp2",
          Placeholder:"A place holder"
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
          Label: "Submit"
        }
      }
    ],
    FormEvent: (eventType: string, FormData?: IFormHost, param?: any) => {
      debugger
      this.Config.Fields[0].ComponentData.Label="xx"
    }
  }

  TableConfig: DataTableConfig = {
    Cells: [
      {
        CellType: CellType.Cell,
        Key: "Name",
        HeaderName:"İsim"
      },
      {
        CellType: CellType.Cell,
        Key: "Surname",
        HeaderName: "Soyisim"
      },
      {
        CellType: CellType.Cell,
        Key: "City",
        HeaderName: "Şehir"
      }
    ],
    Data: [
      
      {
        Name: "Engin",
        Surname: "Özdemir",
        City:"Ankara"
      },
      {
        Name: "Emine",
        Surname: "Yetkin",
        City: "Antalya"
      },
      {
        Name: "Richard",
        Surname: "Lion",
        City: "London"
      }
    ]
    
  }
}
