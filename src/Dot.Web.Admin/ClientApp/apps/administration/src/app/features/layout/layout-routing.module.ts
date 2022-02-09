import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "../../core";
import { NgModule } from "@angular/core";

const routes: Routes = [
  {
    path: "users",
    loadChildren: () => import("../users/users.module").then((m) => m.UsersModule),
    canActivate: [AuthGuard]
  },
  {
    path: "roles",
    loadChildren: () => import("../roles/roles.module").then((m) => m.RolesModule),
    canActivate: [AuthGuard]
  },
  { path: "**", redirectTo: "users" }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule {}
