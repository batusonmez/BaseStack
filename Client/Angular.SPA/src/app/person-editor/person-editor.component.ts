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
import { PersonEditorConfig } from './person-editor.config';
import { PersonService } from '../services/ApiServices/PersonService/person.service';

@Component({
  selector: 'person-editor',
  standalone: true,
  imports: [
    CommonModule,
    FormHostComponent,
    TableHostComponent
  ],
  templateUrl: './person-editor.component.html',
  styleUrls: ['./person-editor.component.scss']
})
export class PersonEditorComponent implements OnInit {


  Config: PersonEditorConfig = {
    FormConfig: {
      Name: "TestForm",
      Fields: [
        {
          Name: "Comp1",
          Component: TextInputComponent,
          ComponentData: {
            Label: "Label comp1"
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
          Key: "Name",
          HeaderName: "İsim"
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
      Commands: [
        {
          Title: "New",
          Class: "btn btn-success",
          Command: { editor: 1 }
        }
      ],
      Data: [ ]
    },
    ShowEditor: false
  }

  constructor(private route: ActivatedRoute, private mapper: RouteMapperService,private personService:PersonService) { }
  ngOnInit(): void {

    this.registerQueryCommands();
    this.personService.GetAllUsers().subscribe(res=>{
        this.Config.DataTableConfig.Data=res;
    })
  }


  registerQueryCommands(): void {
    this.mapper.registerQueryParams(this.route, [
      {
        parameter: "editor",
        action: (prm: string) => {
          if (prm == "1") {
            this.NewPerson(prm);
            this.ToggleEditor(true);
          } else {
            this.ToggleEditor(false);
          }

        }
      }
    ])
  }

  NewPerson(prm: string): void {


  }

  ToggleEditor(show: boolean): void {
    this.Config.ShowEditor = show;
  }
 
}
