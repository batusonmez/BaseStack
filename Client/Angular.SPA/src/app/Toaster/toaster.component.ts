import { Component } from '@angular/core';
import { ToastService } from '../services/ToastService/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-toaster',
  templateUrl: './toaster.component.html',
  styleUrls: ['./toaster.component.scss'],
  imports:[
    CommonModule
  ]
})
export class ToasterComponent {
  public Visible:boolean=false;
  public Message:string="";
  public Type:string="";

  constructor(private toastService: ToastService) {
    toastService.OnToast().subscribe(t => {
      console.log(t);
      this.Message=t.Message;
      this.Visible=true;
      this.Type=t.Type;
      setTimeout(()=>{this.Done()}, 2800);
    });
  }

  Done():void{
    this.Message="";
    this.Visible=false;
    this.Type="";
  }

  get IsError():boolean{
    return this.Type=="Error";
  }

  get IsSuccess():boolean{
    return this.Type=="Success";
  }
}
