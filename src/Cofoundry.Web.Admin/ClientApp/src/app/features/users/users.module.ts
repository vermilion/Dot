import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "@shared/shared.module";
import { UserCreateComponent } from "./user-create/user-create.component";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { UserResolver } from "./resolvers/user.resolver";
import { UsersComponent } from "./users.component";
import { UsersListComponent } from "./users-list/users-list.component";
import { UsersRoutingModule } from "./users-routing.module";

@NgModule({
  declarations: [
    UsersComponent,
    UsersListComponent,
    UserCreateComponent,
    UserEditComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,

    SharedModule
  ],
  exports: [],
  providers: [
    UserResolver
  ]
})
export class UsersModule { }
