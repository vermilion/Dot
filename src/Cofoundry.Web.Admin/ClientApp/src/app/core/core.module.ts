import { CommonModule } from "@angular/common";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule, Optional, SkipSelf } from "@angular/core";

import { WithCredentialsInterceptor } from "./interceptors/with-credentials.interceptor";
import { UnauthorizedInterceptor } from "./interceptors/unauthorized.interceptor";

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: WithCredentialsInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UnauthorizedInterceptor,
      multi: true,
    },
  ],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() core: CoreModule) {
    if (core) {
      throw new Error("Core Module can only be imported to AppModule.");
    }
  }
}
