import { ActivatedRoute, Router } from "@angular/router";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { AuthenticationService } from "../../core";
import { NzNotificationService } from "ng-zorro-antd/notification";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit, OnDestroy {
  validateForm!: FormGroup;
  isLoading: boolean = false;

  submitForm(): void {
    for (const i in this.validateForm.controls) {
      if (this.validateForm.controls.hasOwnProperty(i)) {
        this.validateForm.controls[i].markAsDirty();
        this.validateForm.controls[i].updateValueAndValidity();
      }
    }
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService,
    private notificationsService: NzNotificationService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }

  login() {
    if (!this.validateForm.valid) {
      return;
    }

    this.isLoading = true;

    const value = this.validateForm.value;
    const returnUrl = this.route.snapshot.queryParams.returnUrl || "";
    this.authService
      .login(value.userName, value.password, value.remember)
      .subscribe({
        next: () => {
          this.router.navigate([returnUrl]);
        },
        error: err => {
          this.notificationsService.error("Error", err.error?.title);
        },
        complete: () => this.isLoading = false
      });
  }

  ngOnDestroy(): void {
  }
}
