import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RoleCreateComponent } from "./role-create/role-create.component";
import { RoleEditComponent } from "./role-edit/role-edit.component";
import { RoleResolver } from "./resolvers/role.resolver";
import { RolesComponent } from "./roles.component";
import { RolesListComponent } from "./roles-list/roles-list.component";
import { RolesRoutingModule } from "./roles-routing.module";
import { SharedModule } from "@shared/shared.module";

@NgModule({
  declarations: [
    RolesComponent,
    RolesListComponent,
    RoleCreateComponent,
    RoleEditComponent
  ],
  imports: [
    CommonModule,
    RolesRoutingModule,

    SharedModule
  ],
  exports: [],
  providers: [
    RoleResolver
  ]
})
export class RolesModule { }
