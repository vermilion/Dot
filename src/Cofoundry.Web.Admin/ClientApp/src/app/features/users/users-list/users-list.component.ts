import { finalize } from "rxjs/operators";

import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { API_BASE_URL } from "@shared/constants";

import { PagingParam, PagingResult, User } from "../../interfaces";

@Component({
  selector: "app-users-list",
  templateUrl: "./users-list.component.html",
  styleUrls: ["./users-list.component.scss"],
})
export class UsersListComponent implements OnInit {

  isLoading: boolean = false;
  items: User[] = [];

  page: number = 1;
  quantity: number = 100;
  total: number;

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private router: Router,
    private activatedRoute: ActivatedRoute,
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
      .post<PagingResult<User>>(`${this.baseUrl}/api/usersApi/getAll`, param)
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(res => {
        this.items = res.items;
        this.total = res.totalItems;
      });
  }

  public addUser() {
    this.router.navigate(["create"], { relativeTo: this.activatedRoute });
  }

  public edit(item: User) {
    this.router.navigate(["user", item.id], { relativeTo: this.activatedRoute });
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
