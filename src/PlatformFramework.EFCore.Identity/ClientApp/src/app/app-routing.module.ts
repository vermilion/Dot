import { RouterModule, Routes } from "@angular/router";

import { FeaturesRoutes } from "./features/features.routes";
import { NgModule } from "@angular/core";

const routes: Routes = [
  {
    path: "login",
    loadChildren: () => import("./auth/login/login.module").then(m => m.LoginModule)
  },
  {
    path: "register",
    loadChildren: () => import("./auth/register/register.module").then(m => m.RegisterModule)
  },
  ...FeaturesRoutes
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    enableTracing: true,
    relativeLinkResolution: "legacy"
})],
  exports: [RouterModule],
})
export class AppRoutingModule { }
