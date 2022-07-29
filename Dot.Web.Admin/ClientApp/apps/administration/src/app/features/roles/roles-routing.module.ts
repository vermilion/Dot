import { RouterModule, Routes } from "@angular/router";

import { NgModule } from "@angular/core";
import { RoleCreateComponent } from "./role-create/role-create.component";
import { RoleEditComponent } from "./role-edit/role-edit.component";
import { RoleResolver } from "./resolvers/role.resolver";
import { RolesListComponent } from "./roles-list/roles-list.component";

const routes: Routes = [
  {
    path: "",
    component: RolesListComponent,
    data: {
      breadcrumb: "Roles List"
    }
  },
  {
    path: "create",
    component: RoleCreateComponent,
    data: {
      breadcrumb: "Role Create"
    }
  },
  {
    path: "role/:id",
    component: RoleEditComponent,
    data: {
      breadcrumb: "Role Edit"
    },
    resolve: {
      role: RoleResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RolesRoutingModule {}
