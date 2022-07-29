import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { CommonModule } from "@angular/common";
import { Error404Component } from "./components/error404/error404.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { NgModule } from "@angular/core";
import { NgZorroModule } from "./ng-zorro.module";
import { NumberDirective } from "./directives/numbers-only.directive";
import { RouterModule } from "@angular/router";

@NgModule({
  declarations: [Error404Component, NumberDirective, LoadingComponent],
  imports: [RouterModule, CommonModule, FormsModule, ReactiveFormsModule, NgZorroModule],
  exports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule, Error404Component, NumberDirective, LoadingComponent, NgZorroModule],
  providers: []
})
export class SharedModule {}
