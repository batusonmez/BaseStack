import { Component } from '@angular/core';
import { SubmitButtonComponent } from './Form/FormComponents/submit-button/submit-button.component';
import { TextInputComponent } from './Form/FormComponents/text-input/text-input.component';
import { FormConfig } from './Form/Models/FormConfig';

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
    ]
  }
}
