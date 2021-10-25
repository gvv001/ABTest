
import { Pipe, PipeTransform } from '@angular/core';
import { dateFormat } from 'highcharts';
import * as moment from 'moment';

@Pipe({
    name: 'dateInLocalFormat'
})
export class DatetrPipe implements PipeTransform {

    constructor() { }

    transform(date:string): String {
        
        date = moment(date, "DD-MM-YYYY").format("MM-DD-YYYY")
        let dateInLocalFormat = moment(Date.parse(date)).format("YYYY")+"-"+moment(Date.parse(date)).format("MM")+"-"+moment(Date.parse(date)).format("DD")

        return dateInLocalFormat;
    }

}