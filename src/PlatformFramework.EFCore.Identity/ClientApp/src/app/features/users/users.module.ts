import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "@shared/shared.module";

import { UserResolver } from "./resolvers/user.resolver";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { UsersListComponent } from "./users-list/users-list.component";
import { UsersRoutingModule } from "./users-routing.module";
import { UsersComponent } from "./users.component";

@NgModule({
  declarations: [
    UsersComponent,
    UsersListComponent,
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