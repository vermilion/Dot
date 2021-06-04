import { RouterModule, Routes } from "@angular/router";

import { NgModule } from "@angular/core";
import { UserCreateComponent } from "./user-create/user-create.component";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { UserResolver } from "./resolvers/user.resolver";
import { UsersListComponent } from "./users-list/users-list.component";

const routes: Routes = [
  {
    path: "",
    component: UsersListComponent,
    data: {
      breadcrumb: "User List"
    },
  },
  {
    path: "create",
    component: UserCreateComponent,
    data: {
      breadcrumb: "User Create"
    },
  },
  {
    path: "user/:id",
    component: UserEditComponent,
    data: {
      breadcrumb: "User Edit"
    },
    resolve: {
      user: UserResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
