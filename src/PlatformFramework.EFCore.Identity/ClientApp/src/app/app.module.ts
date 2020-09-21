import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { environment } from "../environments/environment";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { CoreModule } from "./core/core.module";
import { FeaturesLayoutModule } from "./features/layout/layout.module";
import { API_BASE_URL } from "./shared/constants";
import { NotificationModule } from "./shared/services/notification-service/notification.module";
import { SharedModule } from "./shared/shared.module";

export function getRemoteServiceBaseUrl() {
  return environment.apiUrl;
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    CoreModule,
    AppRoutingModule,
    SharedModule.forRoot(),
    NotificationModule.forRoot(),

    FeaturesLayoutModule,
  ],
  providers: [
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
