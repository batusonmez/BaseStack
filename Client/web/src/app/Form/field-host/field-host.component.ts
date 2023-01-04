import { ChangeDetectorRef, Component, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { FieldConfig } from '../Models/FieldConfig';
@Component({
  standalone: true,
  selector: 'field-host',  
  templateUrl: './field-host.component.html',
  styleUrls: ['./field-host.component.scss']
})
export class FieldHostComponent {
  @Input() public Config!: FieldConfig;
  @ViewChild('dynamicField', { read: ViewContainerRef }) FieldContainer!: ViewContainerRef;

  constructor(private cd: ChangeDetectorRef) {

  }


  ngAfterViewInit(): void {
    
    this.registerField();
  }

  private registerField(): void {
    this.FieldContainer.clear();
    let inst = this.FieldContainer.createComponent(this.Config.Component);
    inst.instance.Config = this.Config;
    this.cd.detectChanges();
  }
}
