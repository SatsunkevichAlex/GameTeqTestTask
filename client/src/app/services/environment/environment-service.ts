import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RatesApiConfig } from './models';

@Injectable({ providedIn: 'root' })
export class EnvironmentService {
  public static Config: any;
  private http: HttpClient;

  public get ratesApiConfig(): RatesApiConfig {
    return EnvironmentService.Config.RatesApi as RatesApiConfig;
  }

  public get supportedCurrencies(): string[] {
    return EnvironmentService.Config.SupportedCurrencies as string[];
  }

  load(http: HttpClient): Promise<any> {
    this.http = http;
    return new Promise((resolve, reject) => {
      this.http
        .get('assets/config.json')
        .subscribe({
          next: (response) => {
            EnvironmentService.Config = response;
            return resolve(this);
          },
          error: (error) => {
            console.error('Cannot load environment configurations');
            return reject(error);
          }
        });
    });
  }
}
