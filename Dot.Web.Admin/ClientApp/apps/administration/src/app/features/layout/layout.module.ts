import { CommonModule } from "@angular/common";
import { FeaturesLayoutComponent } from "./layout.component";
import { LayoutRoutingModule } from "./layout-routing.module";
import { NgModule } from "@angular/core";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";

@NgModule({
  imports: [CommonModule, LayoutRoutingModule, NzLayoutModule, NzBreadCrumbModule, NzMenuModule],
  declarations: [FeaturesLayoutComponent],
  exports: [FeaturesLayoutComponent]
})
export class FeaturesLayoutModule {}
