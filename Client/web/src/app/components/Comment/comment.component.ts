import { Component, Input, OnInit } from '@angular/core';
import { HttpService } from '../../services/base.service';


@Component({
  selector: 'comments',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() ContextID: string | null = null;
  comments: any[] = [];
  comment: string = "";
  constructor(private service: HttpService) { }

  ngOnInit(): void {
    this.service.Get("/Comment/Comment?contextID=" + this.ContextID).then(result => {
      this.comments = result;
    });
  }

  Save(): void {
    let comment = { ContextID: this.ContextID, content: this.comment };
    this.service.Post("/Comment/Comment", comment).then(result => {
      this.comments.unshift(comment);
      this.comment = "";
    });
  }

}
