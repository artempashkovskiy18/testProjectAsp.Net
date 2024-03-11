import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {RouterLink, RouterOutlet} from "@angular/router";
import {UrlModel} from "../models/url.model";
import {HttpClient} from "@angular/common/http";
import {CookieService} from "ngx-cookie-service";

@Component({
  selector: 'app-urls',
  standalone: true,
    imports: [
        FormsModule,
        NgForOf,
        NgIf,
        RouterLink,
        RouterOutlet
    ],
  templateUrl: './urls.component.html',
  styleUrl: './urls.component.css'
})
export class UrlsComponent implements OnInit{
  urls: UrlModel[];
  user: string;
  error: string;
  isUserAdmin: boolean;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {
    this.isUserAdmin = false;
    this.error = "";
    this.user = cookieService.get("email");
    this.urls = [];
  }

  ngOnInit(): void {
    this.getUrls();
    this.isAdmin();
  }

  getUrls() {
    this.http.get<Array<UrlModel>>("http://localhost:5230/Urls/GetUrls").subscribe({
      next: (response) => {
        this.urls = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  deleteUrl(id: number) {
    this.http.delete("http://localhost:5230/Urls/DeleteUrl/" + id).subscribe({
      next: (response) => {
        this.getUrls();
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  addUrl(fullUrl: string) {
    this.user = this.cookieService.get("email");
    if (this.user == "") {
      this.error = "you must be registered to add links";
      return;
    }
    this.http.post("http://localhost:5230/Urls/AddUrl", {
      fullUrl: fullUrl,
      creator: this.user,
      creationDate: Date.now()
    }).subscribe({
      next: (response) => {
        this.getUrls();
      },
      error: (error) => {
        this.error = error.error;
        console.log(error)
      }
    })
  }

  isAdmin() {
    this.http.get<boolean>("http://localhost:5230/User/IsAdmin?email=" + this.user).subscribe({
      next: (response) => {
        this.isUserAdmin = response;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}
