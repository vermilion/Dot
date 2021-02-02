import { Component, Input } from "@angular/core";
import { MunicipalitiesService, MunicipalityModel } from "@shared/services/municipalities.service";

import { DialogRef } from "@shared/services/dialog-service/dialog-ref";

@Component({
  selector: "app-edit-dialog",
  templateUrl: "./edit-dialog.component.html",
  styleUrls: ["./edit-dialog.component.scss"]
})
export class MunicipalityEditDialogComponent {

  isLoading: boolean = false;

  @Input()
  model: MunicipalityModel;

  constructor(
    private municipalitiesService: MunicipalitiesService,
    public dialog: DialogRef) {
  }

  ok() {
    this.isLoading = true;
    this.municipalitiesService.update(this.model.id, this.model)
      .then(() => {
        this.dialog.close();
      })
      .finally(() => this.isLoading = false);
  }

  cancel() {
    this.dialog.cancel();
  }
}
