import { CommonModule } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule, Optional, SkipSelf } from "@angular/core";

import { WithCredentialsInterceptor } from "./interceptors/with-credentials.interceptor";
import { UnauthorizedInterceptor } from "./interceptors/unauthorized.interceptor";

@NgModule({
  declarations: [],
  imports: [CommonModule, HttpClientModule],
  exports: [HttpClientModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: WithCredentialsInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UnauthorizedInterceptor,
      multi: true
    }
  ]
})
export class TlsCoreModule {
  constructor(@Optional() @SkipSelf() core: TlsCoreModule) {
    if (core) {
      throw new Error("Core Module can only be imported to AppModule.");
    }
  }
}
