import { RouterModule, Routes } from "@angular/router";

import { NgModule } from "@angular/core";

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
      },
      { path: "**", redirectTo: "login" }
    ]
  },
  {
    path: "",
    loadChildren: () => import("./features/layout/layout.module").then((m) => m.FeaturesLayoutModule)
  },
  { path: "**", redirectTo: "auth" }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: true
      //enableTracing: true,
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
