import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/base.service';
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
  fields: Field[] = [
    new TextBox("Title", "Title"),
    new TextArea("Description", "Description", 4)
  ];
  constructor(private route: ActivatedRoute, private service: HttpService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.service.Get("/api/BookManagement?id=" + this.id).subscribe(result => {

      });
    }
  }

}
