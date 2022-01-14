import { CommonModule } from "@angular/common";
import { FeaturesLayoutComponent } from "./layout.component";
import { NgModule } from "@angular/core";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { RouterModule } from "@angular/router";
import { TlsSharedModule } from "@client-app/tools";

@NgModule({
  imports: [CommonModule, RouterModule, NzLayoutModule, NzBreadCrumbModule, NzMenuModule, TlsSharedModule],
  declarations: [FeaturesLayoutComponent],
  exports: [FeaturesLayoutComponent]
})
export class FeaturesLayoutModule {}
