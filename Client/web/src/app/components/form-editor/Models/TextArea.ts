import { Field } from './Field';

export class TextArea extends Field {

  public Rows: number;
  constructor(Key: string, Label: string, Rows: number = 2, Required: boolean = true, Value: any = null) {
    super(Key, Label, 'textarea', Required, Value);
    this.Rows = Rows;
  }

}
