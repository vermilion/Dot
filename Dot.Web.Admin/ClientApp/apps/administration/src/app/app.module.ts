import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { NZ_CONFIG, NzConfig } from "ng-zorro-antd/core/config";
import { NZ_I18N, en_US } from "ng-zorro-antd/i18n";

import { API_BASE_URL } from "./shared/constants";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BrowserModule } from "@angular/platform-browser";
import { FeaturesLayoutModule } from "./features/layout/layout.module";
import { FormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { UnauthorizedInterceptor } from "./core/interceptors/unauthorized.interceptor";
import { WithCredentialsInterceptor } from "./core/interceptors/with-credentials.interceptor";
import en from "@angular/common/locales/en";
import { environment } from "../environments/environment";
import { registerLocaleData } from "@angular/common";

registerLocaleData(en);

export function getRemoteServiceBaseUrl() {
  return environment.apiUrl;
}

const ngZorroConfig: NzConfig = {
  notification: { nzPlacement: "bottomRight" },
  datePicker: { nzSuffixIcon: "" }
};

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserAnimationsModule, FormsModule, HttpClientModule, AppRoutingModule, FeaturesLayoutModule],
  providers: [
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl },
    { provide: NZ_I18N, useValue: en_US },
    { provide: NZ_CONFIG, useValue: ngZorroConfig },
    { provide: HTTP_INTERCEPTORS, useClass: WithCredentialsInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UnauthorizedInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
