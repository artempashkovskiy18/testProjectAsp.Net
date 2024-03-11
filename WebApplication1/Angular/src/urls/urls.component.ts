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

  _backendLink = "http://localhost:5230/";
  _getAllUrlsLink = "http://localhost:5230/Urls/GetUrls";
  _deleteUrlLink = "http://localhost:5230/Urls/DeleteUrl/"; //+id
  _addUrlLink = "http://localhost:5230/Urls/AddUrl";
  _checkIfUserAdminLink = "http://localhost:5230/User/IsAdmin?email=";


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
    this.http.get<Array<UrlModel>>(this._getAllUrlsLink).subscribe({
      next: (response) => {
        this.urls = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  deleteUrl(id: number) {
    this.http.delete(this._deleteUrlLink + id).subscribe({
      next: (response) => {
        this.getUrls();
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  addUrl(fullUrl: string) {
    if (this.user == "") {
      this.error = "you must be registered to add links";
      return;
    }
    this.http.post(this._addUrlLink, {
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
    this.http.get<boolean>(this._checkIfUserAdminLink + this.user).subscribe({
      next: (response) => {
        this.isUserAdmin = response;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}
