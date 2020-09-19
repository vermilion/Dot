import { ChangeDetectionStrategy, Component, HostBinding, Input, OnInit } from "@angular/core";
import { animate, keyframes, query, stagger, style, transition, trigger } from "@angular/animations";

import { Notification } from "../notification.interface";
import { NotificationService } from "../notification.service";
import { Observable } from "rxjs";

@Component({
  selector: "app-notification-list",
  templateUrl: "./notification-list.component.html",
  styleUrls: ["./notification-list.component.scss"],
  animations: [
    trigger("notificationListAnimation", [
      transition("* => *", [
        query(":enter", style({ opacity: 0 }), { optional: true }),

        query(":enter", stagger("500ms", [
          animate("500ms cubic-bezier(0, 0, .2, 1)", keyframes([
            style({ height: 0, opacity: 0, transform: "translateX({{fromX}})", offset: 0 }),
            style({ height: "*", opacity: 0, transform: "translateX({{fromX}})", offset: 0.75 }),
            style({ height: "*", opacity: 1, transform: "translateX({{toX}})", offset: 1.0 })
          ]))
        ]), { optional: true }),

        query(":leave", stagger("500ms", [
          animate("500ms cubic-bezier(0, 0, .2, 1)", keyframes([
            style({ height: "*", opacity: 1, transform: "translateX({{toX}})", offset: 0 }),
            style({ height: "*", opacity: 0, transform: "translateX({{fromX}})", offset: 0.9 }),
            style({ height: 0, opacity: 0, transform: "translateX({{fromX}})", offset: 1.0 })
          ]))
        ]), { optional: true })
      ], { params: { fromX: "50%", toX: "0" } })
    ])
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  preserveWhitespaces: false
})
export class NotificationListComponent implements OnInit {

  @Input() align: string = null;
  @Input() maxWidth = "420px";

  notifications$: Observable<Notification[]>;

  trackById = (index: number, item: Notification) => {
    return item.id;
  }

  @HostBinding("style.justify-content") get xAlignment() {
    const align = this.alignment;
    const xDirection = align.x;
    if (xDirection !== "left" && xDirection !== "right") {
      throw new Error(`The [x] can only be "left" || "right".`);
    }

    return xDirection === "left" ? "flex-start" : "flex-end";
  }

  @HostBinding("style.align-items") get yAlignment() {
    const align = this.alignment;
    const yDirection = align.y;
    if (yDirection !== "top" && yDirection !== "bottom") {
      throw new Error(`The [y] can only be "top" || "bottom".`);
    }

    return yDirection === "top" ? "flex-start" : "flex-end";
  }

  get params() {
    return {
      fromX: this.alignment.x === "left" ? "-50%" : "50%",
      toX: "0"
    };
  }

  get alignment() {
    if (!this.align) {
      return { x: "right", y: "top" };
    }

    const directions = this.align.split(" ");

    if (directions.length !== 2) {
      throw new Error("The align variables must have an [y x] format.");
    }

    return {
      x: directions[1].trim(),
      y: directions[0].trim()
    };
  }

  constructor(
    private notifications: NotificationService) {
  }

  ngOnInit() {
    this.notifications$ = this.notifications.items;
  }

  onNotificationDestroy(notification: Notification) {
    this.notifications.delete(notification);
  }
}
