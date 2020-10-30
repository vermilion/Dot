import { Component, Input, OnInit } from "@angular/core";

import { Page } from "./providers/page";

@Component({
  selector: "clr-dg-page-size",
  template: `
    <ng-content></ng-content>
    <div class="clr-select-wrapper">
      <select [class.clr-page-size-select]="true" [(ngModel)]="page.size">
        <option *ngFor="let option of pageSizeOptions" [ngValue]="option">{{ option }}</option>
      </select>
    </div>
  `,
})
export class ClrDatagridPageSize implements OnInit {
  @Input("clrPageSizeOptions") pageSizeOptions: number[];

  constructor(public page: Page) {}

  ngOnInit() {
    if (!this.pageSizeOptions || this.pageSizeOptions.length === 0) {
      this.pageSizeOptions = [this.page.size];
    }
  }
}
