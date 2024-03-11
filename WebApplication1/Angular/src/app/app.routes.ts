import {Routes} from '@angular/router';
import {InfoComponent} from "../info/info.component";
import {UrlsComponent} from "../urls/urls.component";

export const routes: Routes = [
  {
    path: "info/:id",
    component: InfoComponent
  },
  {
    path: "",
    component: UrlsComponent
  }
];
