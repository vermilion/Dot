import { NZ_CONFIG, NzConfig } from "ng-zorro-antd/core/config";
import { NZ_I18N, ru_RU } from "ng-zorro-antd/i18n";

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzCheckboxModule } from "ng-zorro-antd/checkbox";
import { NzDatePickerModule } from "ng-zorro-antd/date-picker";
import { NzDropDownModule } from "ng-zorro-antd/dropdown";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzGridModule } from "ng-zorro-antd/grid";
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzInputNumberModule } from "ng-zorro-antd/input-number";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { NzModalModule } from "ng-zorro-antd/modal";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { NzPopconfirmModule } from "ng-zorro-antd/popconfirm";
import { NzSelectModule } from "ng-zorro-antd/select";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzSpinModule } from "ng-zorro-antd/spin";
import { NzTableModule } from "ng-zorro-antd/table";
import { NzUploadModule } from "ng-zorro-antd/upload";
import { registerLocaleData } from "@angular/common";
import ru from "@angular/common/locales/ru";

registerLocaleData(ru);

const ngZorroConfig: NzConfig = {
  notification: { nzPlacement: "bottomRight" },
  datePicker: { nzSuffixIcon: "" }
};

const ngZorroModules = [
  NzGridModule,
  NzFormModule,
  NzInputModule,
  NzInputNumberModule,
  NzBreadCrumbModule,
  NzCheckboxModule,
  NzSelectModule,
  NzDatePickerModule,
  NzNotificationModule,
  NzModalModule,
  NzButtonModule,
  NzLayoutModule,
  NzMenuModule,
  NzIconModule,
  NzTableModule,
  NzSpinModule,
  NzSpaceModule,
  NzPopconfirmModule,
  NzDropDownModule,
  NzCardModule,
  NzUploadModule
];

@NgModule({
  declarations: [
  ],
  imports: [
    BrowserAnimationsModule,
    ...ngZorroModules
  ],
  exports: [
    ...ngZorroModules
  ],
  providers: [
    { provide: NZ_I18N, useValue: ru_RU },
    { provide: NZ_CONFIG, useValue: ngZorroConfig }
  ]
})
export class TlsNgZorroModule {
}
