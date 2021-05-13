import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { API_BASE_URL } from "@shared/constants";

import { User } from "../../interfaces/user";

@Injectable()
export class UserResolver implements Resolve<any> {

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private http: HttpClient) {
  }

  resolve(route: ActivatedRouteSnapshot) {
    const userId = route.paramMap.get("id");

    return this.http.get<User>(`${this.baseUrl}/api/users/get?id=${userId}`);
  }
}
