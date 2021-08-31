import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/base.service';
import { Button } from '../form-editor/Models/Button';
import { Field } from '../form-editor/Models/Field'; 
import { TextArea } from '../form-editor/Models/TextArea';
import { TextBox } from '../form-editor/Models/TextBox';

@Component({
  selector: 'book-edit',
  templateUrl: './book.edit.component.html',
  styleUrls: ['./book.edit.component.css']
})
export class BookEditComponent implements OnInit {
  id: string | null = null;
  apiPath: string | null = null;
  fields: Field[] = [];

  constructor(private route: ActivatedRoute, private service: HttpService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      
      this.service.Get("/book/api/BookManagement?id=" + this.id).subscribe(result => {
        debugger
        this.fields = [
          new TextBox("title", "Title", true),
          new TextArea("description", "Description", 4, true),
          new Button( "Save", "submit", "btn btn-primary", "footer" ),
          new Button("Delete", "button", "btn btn-danger", "footer", () => {
            alert(this.id)
          })
        ];

        for (var i = 0; i < this.fields.length; i++) {
          var field = this.fields[i];
          field.Value = result[field.Key]
        }

      });
    }
  }

  OnSubmit(data: any) {
    data.ID = this.id;
    this.service.Post("/book/api/BookManagement", data).subscribe(result => {

    })

  }

}
