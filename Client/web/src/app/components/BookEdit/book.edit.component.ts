import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'book-edit',
  templateUrl: './book.edit.component.html',
  styleUrls: ['./book.edit.component.css']
})
export class BookEditComponent implements OnInit {
  id: string | null = null;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
  }

}
