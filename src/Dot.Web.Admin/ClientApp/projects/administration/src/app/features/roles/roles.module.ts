import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzSelectModule } from "ng-zorro-antd/select";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzTableModule } from "ng-zorro-antd/table";
import { ReactiveFormsModule } from "@angular/forms";
import { RoleCreateComponent } from "./role-create/role-create.component";
import { RoleEditComponent } from "./role-edit/role-edit.component";
import { RoleResolver } from "./resolvers/role.resolver";
import { RolesListComponent } from "./roles-list/roles-list.component";
import { RolesRoutingModule } from "./roles-routing.module";

@NgModule({
  declarations: [
    RolesListComponent,
    RoleCreateComponent,
    RoleEditComponent
  ],
  imports: [
    CommonModule,
    RolesRoutingModule,

    ReactiveFormsModule,
    NzSelectModule,
    NzLayoutModule,
    NzFormModule,
    NzCardModule,
    NzSpaceModule,
    NzTableModule
  ],
  exports: [],
  providers: [
    RoleResolver
  ]
})
export class RolesModule { }
