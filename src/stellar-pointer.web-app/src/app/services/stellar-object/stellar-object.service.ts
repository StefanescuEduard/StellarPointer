import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StellarObjectDesignation } from './stellar-object-designation.dto';

@Injectable()
export class StellarObjectService {
  private readonly apiUrl;

  constructor(private http: HttpClient) {
    this.apiUrl = 'https://localhost:44373/api/planet';
  }

  pointStellarObject(
    stellarObjectDesignation: StellarObjectDesignation
  ): Observable<void> {
    return this.http.post<void>(this.apiUrl, stellarObjectDesignation);
  }
}
