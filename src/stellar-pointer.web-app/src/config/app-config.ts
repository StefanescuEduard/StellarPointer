import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfigModel } from './app-config.model';
import { environment } from 'src/environments/environment';

@Injectable()
export class AppConfig {
  static settings: AppConfigModel;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  loadEnvironmentFile(): Promise<void> {
    const configFile = this.baseUrl.concat(`assets/config/config.${environment.name}.json`);

    return new Promise<void>((resolve, reject) => {
      this.http
        .get(configFile)
        .toPromise()
        .then((response: AppConfigModel) => {
          AppConfig.settings = response;
          resolve();
        })
        .catch((error: any) => {
          reject(`Could not load file '${configFile}': ${JSON.stringify(error)}`);
        });
    });
  }
}
