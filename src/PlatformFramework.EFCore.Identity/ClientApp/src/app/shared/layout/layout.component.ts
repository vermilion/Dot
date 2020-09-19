import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { PageHeaderIconMenu, PageHeaderNavigationItem } from "@ux-aspects/ux-aspects";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.scss"]
})
export class LayoutComponent {

  public user: any;

  header: string;
  menus: PageHeaderIconMenu[] = [
    {
      icon: "hpe-actions",
      dropdown: [
        {
          header: true,
          title: "",//this.user?.name,
          divider: true
        },
        {
          icon: "hpe-logout",
          title: "Log out",
          select: () => {
          }
        }
      ]
    }
  ];

  menuItems: Array<PageHeaderNavigationItem>;

  constructor(
    router: Router,
    private _route: ActivatedRoute) {

    this.menuItems = [];

    // perform initial navigation - required in a hybrid application
    router.initialNavigation();
  }
}