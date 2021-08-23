import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sample1',
  templateUrl: './sample1.component.html',
  styleUrls: ['./sample1.component.css']
})
export class Sample1Component implements OnInit {

  constructor() { }
  data: any[] = [];
  ngOnInit(): void {

   this.data= [
      {
        Name: 'Osman',
        Surname: 'Hakkı',
        Link: '<a href="#"> Link </a>'
      },
      {
        Name: 'İbrahim',
        Surname: 'Deli'
      },
      {
        Name: 'Ya Habbib',
        Surname: 'mmc'
      }
    ]
  }

}
