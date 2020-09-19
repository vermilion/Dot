import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NotificationModule, PageHeaderModule } from "@ux-aspects/ux-aspects";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { CoreModule } from "./core/core.module";
import { DemoApisComponent } from "./demo-apis/demo-apis.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { LayoutModule } from "./shared/layout/layout.module";
import { SharedModule } from "./shared/shared.module";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    DemoApisComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CoreModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule.forRoot(),

    PageHeaderModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
