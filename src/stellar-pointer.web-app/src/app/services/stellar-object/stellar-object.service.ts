import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StellarObjectDesignation } from './stellar-object-designation.dto';

@Injectable()
export class StellarObjectService {
  private readonly apiUrl;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://192.168.0.120:8081/api/planet';
  }

  pointStellarObject(
    stellarObjectDesignation: StellarObjectDesignation
  ): Observable<void> {
    return this.http.post<void>(this.apiUrl, stellarObjectDesignation);
  }
}
