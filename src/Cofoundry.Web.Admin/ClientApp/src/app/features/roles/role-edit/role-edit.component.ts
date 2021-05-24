import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Role } from "@app/features/interfaces";

import { API_BASE_URL } from "@shared/constants";
import { NzNotificationService } from "ng-zorro-antd/notification";

@Component({
  selector: "app-role-edit",
  templateUrl: "./role-edit.component.html",
  styleUrls: ["./role-edit.component.scss"]
})
export class RoleEditComponent implements OnInit {

  private role: Role;

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
      roleId: [null, [Validators.required]],
      title: [null, [Validators.required]]
    });

    this.role = this.activatedRoute.snapshot.data.role as Role;
    this.form.patchValue(this.role);
  }

  save() {
    this.isLoading = true;

    const param = Object.assign(this.role, this.form.value);

    this.http
      .post(`${this.baseUrl}/api/rolesApi/update`, param)
      .subscribe({
        next: res => {
          this.notificationsService.success("Success", "Role saved");
        },
        error: err => {
          console.log(err);
          this.notificationsService.error("Error", err);
        },
        complete: () => this.isLoading = false
      });
  }

  back() {
    this.router.navigate([""], { relativeTo: this.activatedRoute });
  }
}
