import {Component} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {HttpClientModule} from "@angular/common/http";
import {NgForOf, NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {UrlsComponent} from "../urls/urls.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, NgForOf, NgIf, FormsModule, RouterLink, UrlsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
}
