import { RouterModule, Routes } from "@angular/router";

import { NgModule } from "@angular/core";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { UserResolver } from "./resolvers/user.resolver";
import { UsersComponent } from "./users.component";
import { UsersListComponent } from "./users-list/users-list.component";

const routes: Routes = [
  {
    path: "",
    component: UsersComponent,
    children: [{
      path: "",
      component: UsersListComponent
    },
    {
      path: "user/:id",
      component: UserEditComponent,
      resolve: {
        user: UserResolver
      }
    }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
