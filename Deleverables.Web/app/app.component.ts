import { Component } from '@angular/core';
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { OnInit } from '@angular/core';
import { ITechnicalSkill } from './app.model';

@Component({
  selector: 'my-app',
  template: `<h1> Skills </h1><div *ngIf="isLoading">Loading..</div> <br/> <div *ngFor="let item of items"> <br/>{{item.Name}} : {{item.LevelId}}</div>`
})

export class AppComponent implements OnInit {
    public items: Array<ITechnicalSkill>;
    public isLoading: boolean = true;

    constructor(private http: Http) {
       
    }

    ngOnInit(): void {
        this.GetAll()
            .subscribe(data => { this.items = data; this.isLoading = true });
    }

    public GetAll = (): Observable<Array<ITechnicalSkill>> => {
        return this.http.get('http://localhost:61914/api/TechnicalSkill')
            .map((response: Response) => <Array<ITechnicalSkill>>response.json())
            .do(x => console.log(x));
    }
}
