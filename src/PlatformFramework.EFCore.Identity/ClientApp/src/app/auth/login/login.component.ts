import { Subscription } from "rxjs";
import { finalize } from "rxjs/operators";

import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { AuthenticationService } from "../../core";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  username: string = "";
  password: string = "";
  rememberMe: boolean = false;

  loginError = false;
  private subscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService) {
  }

  ngOnInit(): void {
    /*this.subscription = this.authService.user$.subscribe((x) => {
      if (this.route.snapshot.url[0].path === "login") {
        const accessToken = localStorage.getItem("access_token");
        if (x && accessToken) {
          const returnUrl = this.route.snapshot.queryParams["returnUrl"] || "";
          this.router.navigate([returnUrl]);
        }
      } // optional touch-up: if a tab shows login page, then refresh the page to reduce duplicate login
    });*/
  }

  login() {
    if (!this.username || !this.password) {
      return;
    }

    this.isLoading = true;
    const returnUrl = this.route.snapshot.queryParams["returnUrl"] || "";
    this.authService
      .login(this.username, this.password, this.rememberMe)
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(
        () => {
          this.router.navigate([returnUrl]);
        },
        () => { this.loginError = true; }
      );
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
