import { ActivatedRoute, Router } from "@angular/router";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { AuthenticationService } from "../../core";
import { finalize } from "rxjs/operators";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit, OnDestroy {
  validateForm!: FormGroup;

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
    private fb: FormBuilder) {}

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }


  isLoading: boolean = false;
  loginError = false;

  login() {
    if (!this.validateForm.valid) {
      return;
    }

    this.isLoading = true;

    const value = this.validateForm.value;
    const returnUrl = this.route.snapshot.queryParams.returnUrl || "";
    this.authService
      .login(value.userName, value.password, value.remember)
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
  }
}
