import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LoadingComponent } from "./components/loading/loading.component";
import { NgModule } from "@angular/core";
import { NumberDirective } from "./directives/numbers-only.directive";
import { TlsNgZorroModule } from "../ng-zorro.module";

@NgModule({
  declarations: [NumberDirective, LoadingComponent],
  imports: [BrowserAnimationsModule, TlsNgZorroModule],
  exports: [NumberDirective, LoadingComponent, TlsNgZorroModule]
})
export class TlsSharedModule {}
