import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from '../../services/base.service';
import { NotifyService } from '../../services/notify.service';
import { Button } from '../form-editor/Models/Button';
import { Field } from '../form-editor/Models/Field'; 
import { TextArea } from '../form-editor/Models/TextArea';
import { TextBox } from '../form-editor/Models/TextBox';

@Component({
  selector: 'book-view',
  templateUrl: './book.view.component.html',
  styleUrls: ['./book.view.component.css']
})
export class BookViewComponent implements OnInit {
  id: string | null = null;
  book: any; 
  constructor(private route: ActivatedRoute, private router: Router, private service: HttpService, private confirmationService: NotifyService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      
      this.service.Get("/book/api/BookManagement?id=" + this.id).then(result => {
        this.book = result;         
      });
    }
  }   

}
