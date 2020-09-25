import { Component, Input, OnInit } from '@angular/core';
import { StellarObjectService } from '../services/stellar-object/stellar-object.service';
import { StellarObject } from './stellar-object.model';

@Component({
  selector: 'sp-stellar-object',
  templateUrl: './stellar-object.component.html',
  styleUrls: ['./stellar-object.component.scss'],
})
export class StellarObjectComponent implements OnInit {
  @Input() stellarObject: StellarObject;

  constructor(private stellarObjectService: StellarObjectService) {}

  ngOnInit(): void {}

  pointStellarObject(): void {
    this.stellarObjectService.pointStellarObject({
      prefix: this.stellarObject.prefix,
      name: this.stellarObject.name
    }).subscribe();
  }
}
