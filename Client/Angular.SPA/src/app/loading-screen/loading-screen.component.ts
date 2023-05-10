import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingService } from '../services/ApiServices/loading.service';

@Component({
  selector: 'loading-screen',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading-screen.component.html',
  styleUrls: ['./loading-screen.component.scss']
})
export class LoadingScreenComponent {
  constructor(public loadingService:LoadingService) {

  }

}
