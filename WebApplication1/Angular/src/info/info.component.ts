import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {UrlModel} from "../models/url.model";
import {HttpClient} from "@angular/common/http";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-info',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    RouterLink
  ],
  templateUrl: './info.component.html',
  styleUrl: './info.component.css'
})
export class InfoComponent implements OnInit {
  url:UrlModel
  _backendLink = "http://localhost:5230/";
  constructor(
    private http: HttpClient,
    private router: ActivatedRoute
  ) {
    this.url={creationDate: new Date(), creator: "", fullUrl: "", id: 0, shortUrl: ""};
  }

  ngOnInit(): void {
    let id = this.router.snapshot.paramMap.get("id")
    this.http.get<UrlModel>("http://localhost:5230/Urls/GetUrl/" + id).subscribe({
      next: (response) => {
        this.url = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
