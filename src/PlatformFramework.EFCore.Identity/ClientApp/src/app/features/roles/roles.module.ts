import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "@shared/shared.module";

import { RolesRoutingModule } from "./roles-routing.module";
import { RolesComponent } from "./roles.component";

@NgModule({
  declarations: [
    RolesComponent
  ],
  imports: [
    CommonModule,
    RolesRoutingModule,

    SharedModule
  ],
  exports: []
})
export class RolesModule { }