import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { EnvironmentService } from '../environment/environment-service';
import { ExchangeRateResponse } from './models/rates-response.model';

@Injectable({ providedIn: 'root' })
export class HttpService {

  constructor(
    private http: HttpClient,
    private env: EnvironmentService
  ) { }

  getRate(date: Date): Observable<ExchangeRateResponse> {
    let url = this.env.ratesApiConfig.Endpoint
    let resourse = `/rates/exhange-rate?date=${date}`
    return this.http.get<ExchangeRateResponse>(url + resourse);
  }
}
