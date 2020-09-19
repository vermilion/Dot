import { Component } from "@angular/core";
import { PageHeaderIconMenu } from "@ux-aspects/ux-aspects";

import { AuthService } from "./core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styles: [],
})
export class AppComponent {

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
            this.authService.logout();
          }
        }
      ]
    }
  ];

  constructor(public authService: AuthService) { }

  ngOnInit(): void { }

}
