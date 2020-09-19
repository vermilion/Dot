import { LayoutComponent } from "./layout.component";
import { NgModule } from "@angular/core";
import { NotificationModule } from "../services/notification-service/notification.module";
import { PageHeaderModule } from "@ux-aspects/ux-aspects";
import { SharedModule } from "../shared.module";

@NgModule({
  imports: [
    SharedModule,
    PageHeaderModule,
    NotificationModule.forRoot(),
  ],
  providers: [
  ],
  declarations: [
    LayoutComponent
  ],
  exports: [
    LayoutComponent
  ]
})
export class LayoutModule { }
