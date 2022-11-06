import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ServicesgreenService {
  urlBase: string;
  constructor(private http: HttpClient, @Inject("BASE_URL") url: string) {
    this.urlBase = url;
  }
  public GetSearchCity(city): Observable<any> {
    var url = this.urlBase + "Api/SearchCity/";
    return this.http.get(url + city);


  }
  public SetSaveContact(t) {
    var url = this.urlBase + "Api/ContactSave/";
    return this.http.post(url, t);
  }



}
