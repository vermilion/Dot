import { Component, OnInit } from "@angular/core";

import { ActivatedRoute } from "@angular/router";
import { User } from "@app/core";

@Component({
  selector: "app-user-edit",
  templateUrl: "./user-edit.component.html",
  styleUrls: ["./user-edit.component.scss"]
})
export class UserEditComponent implements OnInit {

  user: User;

  constructor(
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.user = this.route.snapshot.data.user;;
  }
}
