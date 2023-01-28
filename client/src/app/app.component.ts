import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { dateVaidator } from './shared/date.validator';
import { EnvironmentService } from './services/environment/environment-service';
import { HttpService } from './services/http/http-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  supportedCurrencies: string[];

  get selectedDate() { return this.rateFrom.get('selectedDate')!; }
  get selectedCurrency() { return this.rateFrom.get('selectedCurrency')!; }

  rateFrom: FormGroup;
  requestError: string;
  rate: any;

  constructor(
    private env: EnvironmentService,
    private http: HttpService
  ) { }

  ngOnInit() {
    this.rateFrom = new FormGroup({
      selectedDate: new FormControl(
        '', dateVaidator
      ),
      selectedCurrency: new FormControl(
        '', Validators.required
      )
    });

    this.supportedCurrencies = this.env.supportedCurrencies
      .sort(function (a, b) {
        if (a < b) { return -1; }
        if (a > b) { return 1; }
        return 0;
      })
  }

  onSelectedDate() {
    this.requestRate();
  }

  onSelectedCurrency() {
    this.requestRate();
  }

  requestRate() {
    let date = <Date><any>this.selectedDate.value;
    let currency = <string><any>this.selectedCurrency.value;
    if ((this.selectedDate.valid && this.selectedCurrency.valid) &&
      (date && currency)) {
      this.http.getRate(date).subscribe({
        next: (res) => {
          this.rate = (res as any)[currency.toLowerCase()]
          this.requestError = '';
        },
        error: (err) => this.requestError = err.error
      });
    }
  }
}
