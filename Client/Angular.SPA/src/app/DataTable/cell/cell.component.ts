import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { Component, Input, OnInit, SecurityContext } from '@angular/core';
import { CellConfig } from '../Models/CellConfig'; 

@Component({
  standalone: true,
  selector: 'cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss'],
  imports: [CommonModule]
})
export class CellComponent implements OnInit {
  @Input() public Config!: CellConfig;
  @Input() public Data: any;
  BindData: any = "";
  constructor(private sanitizer: DomSanitizer) {

  }

  ngOnInit() {
    this.BindData = this.sanitizer.bypassSecurityTrustHtml( this.Data[this.Config.Binder]+"");
  }


}
