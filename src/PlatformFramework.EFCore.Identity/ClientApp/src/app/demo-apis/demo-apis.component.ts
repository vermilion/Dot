import { finalize } from "rxjs/operators";
import { environment } from "src/environments/environment";

import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: 'app-demo-apis',
  templateUrl: './demo-apis.component.html',
  styleUrls: ['./demo-apis.component.scss'],
})
export class DemoApisComponent implements OnInit {
  private readonly apiUrl = `${environment.apiUrl}api/values`;
  busy = false;
  values: string[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit(): void {}
  getValues() {
    this.busy = true;
    this.http
      .get<string[]>(this.apiUrl)
      .pipe(finalize(() => (this.busy = false)))
      .subscribe((x) => {
        this.values = x;
      });
  }
}
