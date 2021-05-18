import { finalize } from "rxjs/operators";

import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { API_BASE_URL } from "@shared/constants";

import { PagingParam, PagingResult, Role } from "../interfaces";

@Component({
  selector: "app-roles",
  templateUrl: "./roles.component.html",
  styleUrls: ["./roles.component.scss"],
})
export class RolesComponent implements OnInit {

  isLoading: boolean = false;
  items: Role[] = [];

  page: number = 1;
  quantity: number = 100;
  total: number;

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private http: HttpClient) {
  }

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
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(res => {
        this.items = res.items;
        this.total = res.totalItems;
      });
  }

  public edit(item: any) {
    /*this.dialogService.open(MunicipalityEditDialogComponent, {
      context: {
        model: JSON.parse(JSON.stringify(item))
      }
    });*/
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
