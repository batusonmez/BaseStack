import { Field } from './Field';

export class TextBox extends Field {

  constructor(Key: string, Label: string, Required: boolean=true, Value: any = null) {
    super(Key, Label, 'textbox', Required, Value);    
    
  } 

}
