import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "./core";
import { DemoApisComponent } from "./demo-apis/demo-apis.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { LayoutComponent } from "./shared/layout/layout.component";

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  { path: 'login', component: LoginComponent },
  { path: 'demo-apis', component: DemoApisComponent, canActivate: [AuthGuard] },
  {
    path: 'management',
    component: LayoutComponent,
    loadChildren: () => import('./management/management.module').then((m) => m.ManagementModule),
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
