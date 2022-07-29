import { finalize } from "rxjs/operators";

import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { PagingParam, PagingResult, Role } from "../../interfaces";
import { API_BASE_URL } from "../../../shared/constants";

@Component({
  selector: "app-roles-list",
  templateUrl: "./roles-list.component.html",
  styleUrls: ["./roles-list.component.scss"]
})
export class RolesListComponent implements OnInit {
  isLoading = false;
  items: Role[] = [];

  page = 1;
  quantity = 100;
  total?: number;

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.reload();
  }

  public reload() {
    this.isLoading = true;

    const param: PagingParam = {
      pageSize: this.quantity,
      pageNumber: this.page
    };

    this.http
      .post<PagingResult<Role>>(`${this.baseUrl}/api/rolesApi/getAll`, param)
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe((res) => {
        this.items = res.items;
        this.total = res.totalItems;
      });
  }

  public addRole() {
    this.router.navigate(["create"], { relativeTo: this.activatedRoute });
  }

  public edit(item: Role) {
    this.router.navigate(["role", item.roleId], { relativeTo: this.activatedRoute });
  }

  public remove(id: number) {
    /*this.isLoading = true;
    this.municipalitiesService.delete(id)
      .then(() => {
        this.reload();
      })
      .finally(() => this.isLoading = false);*/
  }
}
