import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { User } from "../../features/interfaces";
import { API_BASE_URL } from "../../shared/constants";

@Injectable({ providedIn: "root" })
export class AuthenticationService {
  //public user: Observable<User>;

  constructor(@Inject(API_BASE_URL) private baseUrl: string, private router: Router, private http: HttpClient) {}

  login(userName: string, password: string, rememberMe: boolean) {
    return this.http.post<User>(`${this.baseUrl}/api/auth/login`, {
      userName,
      password,
      rememberMe
    });
  }

  logout() {
    this.http.post(`${this.baseUrl}/api/auth/logout`, {}).subscribe(() => {
      this.redirectToLogin();
    });
  }

  redirectToLogin() {
    this.router.navigate(["/auth/login"]);
  }
}
