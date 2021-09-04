import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from '../../services/base.service';
import { NotifyService } from '../../services/notify.service';
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

  constructor(private route: ActivatedRoute, private router: Router, private service: HttpService, private confirmationService: NotifyService) { }

  ngOnInit(): void {
    debugger
    this.id = this.route.snapshot.paramMap.get('id');
   let filelds = [
      new TextBox("title", $localize`:@@be.Title:Name`, true),
      new TextArea("description", $localize`:@@be.Description:Description`, 4, true),
      new Button($localize`:@@be.Save:Save`, "submit", "btn btn-primary", "footer"),

    ];
    if (this.id) {
      filelds.push(new Button($localize`:@@be.Delete:Delete`, "button", "btn btn-danger", "footer", () => {
        this.confirmationService.Confirm($localize`:@@be.Confirm:Are You Sure?`, () => {
          this.service.Delete("/book/api/BookManagement?id=" + this.id).then(result => {
            this.Navigate("/books");
          })
        });

      }));
      this.service.Get("/book/api/BookManagement?id=" + this.id).then(result => {
        this.fields = filelds;
        for (var i = 0; i < this.fields.length; i++) {
          var field = this.fields[i];
          field.Value = result[field.Key]
        }

      });
    } else {
      this.fields = filelds;
    }
  }

  Navigate(url: string): void {
    this.router.navigateByUrl(url);
  }

  OnSubmit(data: any) {
    if (this.id) {
      data.id = this.id;
    }
    
    this.service.Post("/book/api/BookManagement", data).then(result => {

    })

  }

}
