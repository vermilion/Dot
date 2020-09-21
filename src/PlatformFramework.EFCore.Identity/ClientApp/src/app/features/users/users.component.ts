import { finalize } from "rxjs/operators";

import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { API_BASE_URL } from "@shared/constants";

import { PagingParam, PagingResult, User } from "../interfaces";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit {

  isLoading: boolean = false;
  items: User[] = [];

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

    var param: PagingParam = {
      limit: this.quantity,
      offset: (this.page - 1) * this.quantity
    };

    this.http
      .post<PagingResult<User>>(`${this.baseUrl}/api/users/getAll`, param)
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(res => {
        this.items = res.collection;
        this.total = res.total;
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
