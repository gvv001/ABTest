import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { User } from './../data-types';

@Injectable()
export class DataService {

  constructor(private http: HttpClient, private router: Router) { }


  public urlString: string = "http://195.133.48.152:5000";
  //public urlString: string = "http://localhost:5000";



  goToUrl(url: string) {

    this.router.navigate(
      [url]
    );
  }



  loadData() {
    console.log("loadData()...")
  }


  postUsers(users: User[], url: String) {
    return this.http.post(this.urlString + url, users);
  }



}