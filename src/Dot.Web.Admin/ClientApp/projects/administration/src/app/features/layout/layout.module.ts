import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SharedModule } from "@shared/shared.module";

import { FeaturesLayoutComponent } from "./layout.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  providers: [
  ],
  declarations: [
    FeaturesLayoutComponent
  ],
  exports: [
    FeaturesLayoutComponent,
  ]
})
export class FeaturesLayoutModule { }
