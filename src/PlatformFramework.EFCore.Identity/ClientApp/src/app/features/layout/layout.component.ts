import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthenticationService, User } from "@app/core";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.scss"]
})
export class FeaturesLayoutComponent {

  public user: User;

  header: string;
  menus: any[] = [
    {
      icon: "hpe-actions",
      dropdown: [
        {
          header: true,
          title: "", //this.user?.name,
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

  menuItems: Array<any>;

  categories = [
    {
      link: "users",
      title: "Users"
    },
    {
      link: "roles",
      title: "Roles"
    }
  ];

  constructor(
    router: Router,
    private authenticationService: AuthenticationService,
    private _route: ActivatedRoute) {

    this.header = "Identity Admin";
    this.menuItems = [];

    this.authenticationService.user.subscribe(x => this.user = x);

    // perform initial navigation - required in a hybrid application
    router.initialNavigation();
  }

  logout() {
    this.authenticationService.logout();
  }
}