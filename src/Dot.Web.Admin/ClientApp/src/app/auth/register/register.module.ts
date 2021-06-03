import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RegisterComponent } from "./register.component";
import { RegisterRoutingModule } from "./register-routing.module";
import { SharedModule } from "@shared/shared.module";

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RegisterRoutingModule,

    SharedModule
  ]
})
export class RegisterModule { }