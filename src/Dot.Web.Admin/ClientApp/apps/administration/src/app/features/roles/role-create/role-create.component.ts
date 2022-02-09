import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import { NzNotificationService } from "ng-zorro-antd/notification";
import { API_BASE_URL } from "../../../shared/constants";

@Component({
  selector: "app-role-create",
  templateUrl: "./role-create.component.html",
  styleUrls: ["./role-create.component.scss"]
})
export class RoleCreateComponent implements OnInit {
  form!: FormGroup;
  isLoading = false;
  rolesList: any[] = [];

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private notificationsService: NzNotificationService,
    private router: Router,
    private fb: FormBuilder,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      roleId: [0, [Validators.required]],
      title: [null, [Validators.required]]
    });
  }

  create() {
    this.isLoading = true;

    const param = Object.assign(this.form.value);

    this.http.post(`${this.baseUrl}/api/rolesApi/add`, param).subscribe({
      next: (res) => {
        this.notificationsService.success("Success", "Role added");
        this.back();
      },
      error: (err) => {
        console.log(err);
        this.notificationsService.error("Error", err);
      },
      complete: () => (this.isLoading = false)
    });
  }

  back() {
    this.router.navigate(["roles"]);
  }
}
