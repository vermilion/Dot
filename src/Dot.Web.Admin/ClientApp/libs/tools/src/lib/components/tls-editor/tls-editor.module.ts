import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AngularEditorComponent } from "./angular-editor.component";
import { AngularEditorToolbarComponent } from "./ag-editor-toolbar.component";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { TlsNgZorroModule } from "../../ng-zorro.module.ts";
import { TlsSelectComponent } from "./ag-select/ag-select.component";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TlsNgZorroModule
  ],
  declarations: [
    AngularEditorComponent,
    AngularEditorToolbarComponent,
    TlsSelectComponent
  ],
  exports: [
    AngularEditorComponent,
    AngularEditorToolbarComponent
  ]
})
export class TlsEditorModule {
}
