import { Routes } from "@angular/router";

import { AuthGuard } from "../core";
import { FeaturesLayoutComponent } from "./layout/layout.component";

export const FeaturesRoutes: Routes = [
  {
    path: "main",
    component: FeaturesLayoutComponent,
    children: [
      {
        path: "users",
        loadChildren: () => import("./users/users.module").then(m => m.UsersModule),
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: "roles",
        loadChildren: () => import("./roles/roles.module").then(m => m.RolesModule),
        canActivate: [
          AuthGuard
        ]
      }
    ]
  },
  { path: "**", redirectTo: "main/users" },
];