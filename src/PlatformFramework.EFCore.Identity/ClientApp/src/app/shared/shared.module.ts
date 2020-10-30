import { CommonModule } from "@angular/common";
import { ModuleWithProviders, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

import { ClarityModule } from "./components/clr-angular/clr-angular.module";
import { Error404Component } from "./components/error404/error404.component";
import { NumberDirective } from "./directives/numbers-only.directive";
import { DialogModule } from "./services/dialog-service/dialog.module";

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,

    ClarityModule
  ],
  providers: [
  ],
  declarations: [
    Error404Component,
    NumberDirective,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    Error404Component,
    NumberDirective,
    DialogModule,

    ClarityModule
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    return {
      ngModule: SharedModule
    };
  }
}
