import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";

import { RegisterRoutingModule } from "./register-routing.module";
import { RegisterComponent } from "./register.component";

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RegisterRoutingModule,
  ]
})
export class RegisterModule { }