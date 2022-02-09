import { CommonModule } from "@angular/common";
import { LoginComponent } from "./login.component";
import { LoginRoutingModule } from "./login-routing.module";
import { NgModule } from "@angular/core";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [LoginComponent],
  imports: [CommonModule, LoginRoutingModule, ReactiveFormsModule, NzNotificationModule, NzInputModule, NzButtonModule, NzCardModule, NzFormModule]
})
export class LoginModule {}
