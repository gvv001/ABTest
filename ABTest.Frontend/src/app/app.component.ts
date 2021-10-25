import { Component, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { interval, Subscription, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { DataService } from './services/data.service';
import { User } from './data-types';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NgForm } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { DatetrPipe } from './pipes/datetr.pipe';


import * as Highcharts from 'highcharts';
declare var UIkit: any;

import * as moment from 'moment';




// Для инициализации highcharts
declare var require: any;
const More = require('highcharts/highcharts-more');
More(Highcharts);

import Histogram from 'highcharts/modules/histogram-bellcurve';
import { CDK_DESCRIBEDBY_ID_PREFIX } from '@angular/cdk/a11y';
Histogram(Highcharts);

const Exporting = require('highcharts/modules/exporting');
Exporting(Highcharts);

const ExportData = require('highcharts/modules/export-data');
ExportData(Highcharts);

const Accessibility = require('highcharts/modules/accessibility');
Accessibility(Highcharts);





@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})


export class AppComponent implements AfterViewInit, OnInit {



  onChangedLastActivity(event: any, index: any) {

    let date = moment(event.target.value, "YYYY-MM-DD").format("DD.MM.YYYY");
    this.Users[index].dateLastActivity = date;
  }

  onChangedRegistration(event: any, index: any) {

    let date = moment(event.target.value, "YYYY-MM-DD").format("DD.MM.YYYY");
    this.Users[index].dateRegistration = date;
  }

  activity: any;
  xData: any;
  label: any;
  isVisible:Boolean=true;
  options: any;



  dataSource: MatTableDataSource<User>;

  Users: User[];
  displayedColumns: string[] = ['userID', 'dateRegistration', 'dateLastActivity'];
  rollingRetentionDay: Number = 7;
  rollingRetention: String;
  usersLifeSpan: any;
  today: Date;


  constructor(private http: HttpClient, private dataService: DataService, private activateRoute: ActivatedRoute, public router: Router) {

    this.today = new Date();

    this.options = {
      chart: {
        type: 'column'
      },
      title: {
        text: 'Users Lifespan'
      },

      xAxis: {
        type: 'category',
        labels: {
          rotation: -45,
          style: {
            fontSize: '13px',
            fontFamily: 'Verdana, sans-serif'
          }
        }
      },

      yAxis: {
        min: 0,
        title: {
          text: 'Users'
        }
      },
      series: [{
        name: 'LifeSpan',
        //data: this.usersLifeSpan,
        // dataLabels: {
        //   enabled: true,
        //   rotation: -90,
        //   color: '#FFFFFF',
        //   align: 'right',
        //   format: '{point.y:.1f}', // one decimal
        //   y: 100, // 10 pixels down from the top
        //   style: {
        //     fontSize: '13px',
        //     fontFamily: 'Verdana, sans-serif'
        //   }
        // }
      }]
    };
  }


  checkDates(): Boolean {

    let result: Boolean = true;

    this.Users.forEach(u => {

      if (moment(u.dateRegistration, "DD.MM.YYYY") > moment(u.dateLastActivity, "DD.MM.YYYY")) {
        this.notify("Пользователь '" + u.userID + "'  Дата регистрации не может быть больше Даты последней активности");
        result = false;
      }

    })

    return result;

  }




  calcStats() {


    this.getRollingRetention();
    this.getUsersLifespan();


  }



  createHistogram() {

    this.options.series[0]['data'] = this.usersLifeSpan;

    Highcharts.chart('container', this.options);

  }



  getRollingRetention() {

    this.http.get(this.dataService.urlString + '/Statistics/RollingRetention?day=' + this.rollingRetentionDay).subscribe(

      (data: any) => {
        this.rollingRetention = data + "%";
      },
      error => {
        this.notify(error.error)
        this.rollingRetention = "ошибка расчёта"
      }

    );
  }




  getUsers() {

    this.http.get(this.dataService.urlString + '/User/GetAll').subscribe((data: any) => {

      this.isVisible=false;
      this.Users = data;

      this.Users.forEach(element => {

        element.dateRegistration = moment(Date.parse(element.dateRegistration)).format("DD.MM.YYYY");
        element.dateLastActivity = moment(Date.parse(element.dateLastActivity)).format("DD.MM.YYYY");

      });

    });

  }


  getUsersLifespan() {

    this.http.get(this.dataService.urlString + '/Statistics/UsersLifespan').subscribe(data => {

      this.usersLifeSpan = data;
      this.createHistogram();

    });

  }



  notify(message: string) {
    UIkit.notification({ message: message })
  }


  saveUsers() {

    if (this.checkDates() == false)
      return;


    this.dataService.postUsers(this.Users, '/User/Save').subscribe(
      (data: any) => { console.log("RETURN" + data); UIkit.modal.alert("Данные сохранены!"); }
      , error => this.notify(error.error)
    );

  }





  ngAfterViewInit() {

    //this.dataSource.sort = this.sort;

  }




  ngOnInit() {

    this.getUsers();
    this.getRollingRetention();
    this.getUsersLifespan();

    //UIkit.datepicker(document.getElementById("datePicker"), { /* options */ });

  }




}
