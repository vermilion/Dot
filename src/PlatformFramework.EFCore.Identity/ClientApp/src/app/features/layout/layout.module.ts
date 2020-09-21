import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { PageHeaderModule } from "@ux-aspects/ux-aspects";

import { FeaturesLayoutComponent } from "./layout.component";

@NgModule({
  imports: [
    CommonModule,
    PageHeaderModule,
    RouterModule
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
