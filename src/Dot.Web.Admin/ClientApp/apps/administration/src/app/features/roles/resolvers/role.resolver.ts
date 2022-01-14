import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { API_BASE_URL } from "../../../shared/constants";
import { Role } from "../../interfaces";

@Injectable()
export class RoleResolver implements Resolve<Role> {
  constructor(@Inject(API_BASE_URL) private baseUrl: string, private http: HttpClient) {}

  resolve(route: ActivatedRouteSnapshot) {
    const roleId = route.paramMap.get("id");

    return this.http.get<Role>(`${this.baseUrl}/api/rolesApi/getById?roleId=${roleId}`);
  }
}
