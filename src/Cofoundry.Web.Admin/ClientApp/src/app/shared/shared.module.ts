import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { CommonModule } from "@angular/common";
import { Error404Component } from "./components/error404/error404.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { NgModule } from "@angular/core";
import { NumberDirective } from "./directives/numbers-only.directive";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCheckboxModule } from "ng-zorro-antd/checkbox";
import { NzDatePickerModule } from "ng-zorro-antd/date-picker";
import { NzDropDownModule } from "ng-zorro-antd/dropdown";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzGridModule } from "ng-zorro-antd/grid";
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzInputNumberModule } from "ng-zorro-antd/input-number";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { NzModalModule } from "ng-zorro-antd/modal";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { NzPopconfirmModule } from "ng-zorro-antd/popconfirm";
import { NzSelectModule } from "ng-zorro-antd/select";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzSpinModule } from "ng-zorro-antd/spin";
import { NzTableModule } from "ng-zorro-antd/table";
import { RouterModule } from "@angular/router";

const ngZorroModules = [
  NzGridModule,
  NzFormModule,
  NzInputModule,
  NzInputNumberModule,
  NzBreadCrumbModule,
  NzCheckboxModule,
  NzSelectModule,
  NzDatePickerModule,
  NzNotificationModule,
  NzModalModule,
  NzButtonModule,
  NzLayoutModule,
  NzMenuModule,
  NzIconModule,
  NzTableModule,
  NzSpinModule,
  NzSpaceModule,
  NzPopconfirmModule,
  NzDropDownModule
];

@NgModule({
  declarations: [
    Error404Component,
    NumberDirective,
    LoadingComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    ...ngZorroModules
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    Error404Component,
    NumberDirective,
    LoadingComponent,

    ...ngZorroModules
  ],
  providers: [
  ]
})
export class SharedModule {
}
