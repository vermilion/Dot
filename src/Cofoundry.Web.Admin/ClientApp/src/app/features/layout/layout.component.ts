import {
  ActivatedRoute,
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
  RouterOutlet
} from "@angular/router";
import { AuthenticationService, User } from "@app/core";

import { Component } from "@angular/core";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.scss"]
})
export class FeaturesLayoutComponent {

  public user: User;
  isLoading: boolean = false;

  constructor(
    router: Router,
    private authenticationService: AuthenticationService,
    private _route: ActivatedRoute) {

    this.authenticationService.user.subscribe(x => this.user = x);

    // perform initial navigation - required in a hybrid application
    router.initialNavigation();

    router.events.subscribe(event => {
      switch (true) {
        case event instanceof NavigationStart: {
          this.isLoading = true;
          break;
        }

        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError: {
          this.isLoading = false;
          break;
        }
        default: {
          break;
        }
      }
    });
  }

  logout() {
    this.authenticationService.logout();
  }
}
