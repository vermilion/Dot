import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { NzSelectModule } from "ng-zorro-antd/select";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzTableModule } from "ng-zorro-antd/table";
import { ReactiveFormsModule } from "@angular/forms";
import { UserCreateComponent } from "./user-create/user-create.component";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { UserResolver } from "./resolvers/user.resolver";
import { UsersListComponent } from "./users-list/users-list.component";
import { UsersRoutingModule } from "./users-routing.module";

@NgModule({
  declarations: [UsersListComponent, UserCreateComponent, UserEditComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,

    ReactiveFormsModule,
    NzNotificationModule,
    NzSelectModule,
    NzLayoutModule,
    NzFormModule,
    NzCardModule,
    NzButtonModule,
    NzInputModule,
    NzSpaceModule,
    NzTableModule
  ],
  providers: [UserResolver]
})
export class UsersModule {}
