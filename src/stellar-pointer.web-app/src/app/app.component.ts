import { Component } from '@angular/core';
import { StellarObject } from './planet/stellar-object.model';
import { stellarObjects } from './planet/stellar-objects';
import { StellarObjectService } from './services/stellar-object/stellar-object.service';

@Component({
  selector: 'sp-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [StellarObjectService]
})
export class AppComponent {
  stellarObjects: StellarObject[];

  constructor() {
    this.stellarObjects = stellarObjects;
  }
}
