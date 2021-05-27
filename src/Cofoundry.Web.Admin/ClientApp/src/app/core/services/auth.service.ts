import { User } from "@app/features/interfaces";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";

import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { API_BASE_URL } from "@shared/constants";

@Injectable({ providedIn: "root" })
export class AuthenticationService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string,
    private router: Router,
    private http: HttpClient) {

    this.userSubject = new BehaviorSubject<User>(null);
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(userName: string, password: string, rememberMe: boolean) {
    return this.http
      .post<User>(`${this.baseUrl}/api/auth/login`, {
        userName,
        password,
        rememberMe
      })
      .pipe(
        map(user => {
          this.userSubject.next(user);

          return user;
        })
      );
  }

  logout() {
    this.http
      .post(`${this.baseUrl}/api/auth/logout`, {})
      .subscribe(x => {
        this.userSubject.next(null);
        this.router.navigate(["/login"]);
      });
  }
}
