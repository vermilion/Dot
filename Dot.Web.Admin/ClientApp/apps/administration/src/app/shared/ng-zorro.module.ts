import * as AllIcons from "@ant-design/icons-angular/icons";

import { NZ_CONFIG, NzConfig } from "ng-zorro-antd/core/config";
import { NZ_I18N, en_US } from "ng-zorro-antd/i18n";
import { NZ_ICONS, NzIconModule } from "ng-zorro-antd/icon";

import { IconDefinition } from "@ant-design/icons-angular";
import { NgModule } from "@angular/core";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzCheckboxModule } from "ng-zorro-antd/checkbox";
import { NzDatePickerModule } from "ng-zorro-antd/date-picker";
import { NzDropDownModule } from "ng-zorro-antd/dropdown";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzGridModule } from "ng-zorro-antd/grid";
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
import en from "@angular/common/locales/en";
import { registerLocaleData } from "@angular/common";

registerLocaleData(en);

const ngZorroConfig: NzConfig = {
  notification: { nzPlacement: "bottomRight" },
  datePicker: { nzSuffixIcon: "" }
};

const antDesignIcons = AllIcons as {
  [key: string]: IconDefinition;
};
const icons: IconDefinition[] = Object.keys(antDesignIcons).map((key) => antDesignIcons[key]);

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
  NzCardModule
];

@NgModule({
  declarations: [],
  imports: [...ngZorroModules],
  exports: [...ngZorroModules],
  providers: [
    { provide: NZ_ICONS, useValue: icons },
    { provide: NZ_I18N, useValue: en_US },
    { provide: NZ_CONFIG, useValue: ngZorroConfig }
  ]
})
export class NgZorroModule {}
