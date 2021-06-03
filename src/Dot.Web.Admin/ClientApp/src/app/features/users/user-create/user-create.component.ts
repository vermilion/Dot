import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import { API_BASE_URL } from "@shared/constants";
import { NzNotificationService } from "ng-zorro-antd/notification";

@Component({
  selector: "app-user-create",
  templateUrl: "./user-create.component.html",
  styleUrls: ["./user-create.component.scss"]
})
export class UserCreateComponent implements OnInit {

  form!: FormGroup;
  isLoading: boolean = false;
  rolesList: any[] = [];

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private notificationsService: NzNotificationService,
    private router: Router,
    private fb: FormBuilder,
    private http: HttpClient) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      userId: [0, [Validators.required]],
      username: [null, [Validators.required]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      email: [null, [Validators.required]],
      role: this.fb.group({
        roleId: [null, [Validators.required]]
      })
    });

    this.fetchRoles();
  }

  create() {
    this.isLoading = true;

    this.http
      .post(`${this.baseUrl}/api/usersApi/add`, this.form.value)
      .subscribe({
        next: res => {
          this.notificationsService.success("Success", "User added");
          this.back();
        },
        error: err => {
          console.log(err);
          this.notificationsService.error("Error", err);
        },
        complete: () => this.isLoading = false
      });
  }

  back() {
    this.router.navigate(["users"]);
  }

  private fetchRoles() {
    this.isLoading = true;

    this.http
      .post<any>(`${this.baseUrl}/api/rolesApi/getAll`, {
        excludeAnonymous: true
      })
      .subscribe({
        next: res => {
          this.rolesList = res.items;
        },
        error: err => {
          console.log(err);
        },
        complete: () => this.isLoading = false
      });
  }
}
