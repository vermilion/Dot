import { RouterModule, Routes } from "@angular/router";

import { NgModule } from "@angular/core";
import { featuresRoutes } from "./features/features.routes";

const routes: Routes = [
  {
    path: "auth",
    children: [
      {
        path: "login",
        loadChildren: () => import("./auth/login/login.module").then((m) => m.LoginModule)
      },
      {
        path: "register",
        loadChildren: () => import("./auth/register/register.module").then((m) => m.RegisterModule)
      }
    ]
  },
  ...featuresRoutes
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      //enableTracing: true,
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
