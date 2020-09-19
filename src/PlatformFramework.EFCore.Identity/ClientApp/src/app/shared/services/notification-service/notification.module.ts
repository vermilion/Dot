import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { NotificationItemComponent } from "./notification-item/notification-item.component";
import { NotificationListComponent } from "./notification-list/notification-list.component";
import { NotificationOptions } from "./notification.interface";
import { NotificationService } from "./notification.service";

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    NotificationListComponent,
    NotificationItemComponent
  ],
  exports: [
    NotificationListComponent
  ]
})
export class NotificationModule {

  static forRoot(options: NotificationOptions = {}) {
    return {
      ngModule: NotificationModule,
      providers: [
        NotificationService,
        { provide: NotificationOptions, useValue: options }
      ]
    };
  }
}
