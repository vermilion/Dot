import { AfterViewInit, ChangeDetectorRef, Component, ComponentFactoryResolver, ComponentRef, OnDestroy, Type, ViewChild, ViewContainerRef } from "@angular/core";
import { animate, state, style, transition, trigger } from "@angular/animations";

import { DialogConfig } from "./dialog-config";
import { DialogRef } from "./dialog-ref";
import { Subject } from "rxjs";

@Component({
  selector: "app-dialog",
  templateUrl: "./dialog.component.html",
  styleUrls: ["./dialog.component.scss"],
  animations: [
    trigger("dialog", [
      state("in", style({ opacity: 1 })),
      state("void", style({ opacity: 0 })),
      transition("void => *", [
        style({ opacity: 0 }),
        animate(200)
      ])
    ])
  ]
})
export class DialogComponent implements AfterViewInit, OnDestroy {
  componentRef: ComponentRef<any>;

  @ViewChild("point", { static: true, read: ViewContainerRef }) viewContainerRef: ViewContainerRef;

  private readonly _onClose = new Subject<any>();
  public onClose = this._onClose.asObservable();

  /**
   * Dialog inner component type
   */
  childComponentType: Type<any>;

  /**
   * Dialog configuration
   */
  config: DialogConfig;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private cd: ChangeDetectorRef,
    private dialogRef: DialogRef) {
  }

  ngAfterViewInit(): void {
    this.loadChildComponent(this.childComponentType);
    this.cd.detectChanges();
  }

  onOverlayClicked(evt: MouseEvent) {
    if (this.config.closeOnOverlayClick)
      this.dialogRef.close();
  }

  onDialogClicked(evt: MouseEvent) {
    evt.stopPropagation();
  }

  loadChildComponent(componentType: Type<any>) {
    let componentFactory = this.componentFactoryResolver.resolveComponentFactory(componentType);

    let viewContainerRef = this.viewContainerRef;
    viewContainerRef.clear();

    this.componentRef = viewContainerRef.createComponent(componentFactory);

    //apply config
    if (this.config.context) {
      Object.assign(this.componentRef.instance, { ...this.config.context });
    }
  }

  ngOnDestroy() {
    if (this.componentRef) {
      this.componentRef.destroy();
    }
  }

  close() {
    this._onClose.next();
  }
}
