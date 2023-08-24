import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { Component, Input, OnInit, SecurityContext } from '@angular/core';
import { CellConfig } from '../Models/CellConfig'; 
import { RouterModule } from '@angular/router';
import { CellType } from '../Models/CellType';

@Component({
  standalone: true,
  selector: 'cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss'],
  imports: [CommonModule,RouterModule]
})
export class CellComponent implements OnInit {
  @Input() public Config!: CellConfig;
  @Input() public Data: any;
  BindData: any = "";
  CellType=CellType;
  
  constructor(private sanitizer: DomSanitizer) {

  }

  ngOnInit() {
    if(!this.Config.CellType){
      this.Config.CellType=CellType.Template;
    }

    let content=this.Data[this.Config.Binder];
    switch (this.Config.CellType) {
      case CellType.Template:
        this.BindData = this.sanitizer.bypassSecurityTrustHtml(content);  
        break;
    case CellType.Link:
      
      this.BindData=content;
      break;
      default:
        break;
    }
 
   
  }


}
