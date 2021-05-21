import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

import { API_BASE_URL } from "@shared/constants";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { finalize } from "rxjs/operators";

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
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private http: HttpClient) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      userName: [null, [Validators.required]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      email: [null, [Validators.required]],
      roleId: [null, [Validators.required]],
    });

    this.fetchRoles();
  }

  create() {
    this.isLoading = true;

    const param = Object.assign(this.form.value, { roleId: 1 });

    this.http
      .post(`${this.baseUrl}/api/usersApi/add`, param)
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(res => {
        this.notificationsService.success("Success", "User added");
        this.router.navigate([""], { relativeTo: this.activatedRoute });
      }, err => {
        console.log(err);
        this.notificationsService.error("Error", err);
      });
  }

  private fetchRoles() {
    this.isLoading = true;

    this.http
      .post<any>(`${this.baseUrl}/api/rolesApi/getAll`, {
        excludeAnonymous: true
      })
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(res => {
        this.rolesList = res.items;
      }, err => {
        console.log(err);
      });
  }
}
