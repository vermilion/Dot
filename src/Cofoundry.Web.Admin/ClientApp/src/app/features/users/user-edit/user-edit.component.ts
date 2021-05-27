import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { UserSummary } from "@app/features/interfaces";

import { API_BASE_URL } from "@shared/constants";
import { NzNotificationService } from "ng-zorro-antd/notification";

@Component({
  selector: "app-user-edit",
  templateUrl: "./user-edit.component.html",
  styleUrls: ["./user-edit.component.scss"]
})
export class UserEditComponent implements OnInit {

  form!: FormGroup;
  isLoading: boolean = false;
  rolesList: any[] = [];

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private notificationsService: NzNotificationService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private http: HttpClient) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      userId: [null, [Validators.required]],
      username: [null, [Validators.required]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      email: [null, [Validators.required]],
      role: this.fb.group({
        roleId: [null, [Validators.required]]
      })
    });

    this.fetchRoles();

    const user = this.activatedRoute.snapshot.data.user as UserSummary;
    this.form.patchValue(user);
  }

  save() {
    this.isLoading = true;

    const param = Object.assign(this.form.value, { roleId: 1 });

    this.http
      .post(`${this.baseUrl}/api/usersApi/update`, param)
      .subscribe({
        next: res => {
          this.notificationsService.success("Success", "User saved");
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

