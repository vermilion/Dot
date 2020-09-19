import { ApplicationRef, ComponentFactoryResolver, ComponentRef, EmbeddedViewRef, Injectable, Injector, Type } from "@angular/core";

import { DialogComponent } from "./dialog.component";
import { DialogConfig } from "./dialog-config";
import { DialogInjector } from "./dialog-injector";
import { DialogModule } from "./dialog.module";
import { DialogRef } from "./dialog-ref";

@Injectable({
  providedIn: DialogModule
})
export class DialogService {
  dialogComponentRef: ComponentRef<DialogComponent>;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector) {
  }

  public open<T>(componentType: Type<T>, config: DialogConfig<Partial<T>>) {
    const dialogRef = this.appendDialogComponentToBody();

    this.dialogComponentRef.instance.childComponentType = componentType;
    this.dialogComponentRef.instance.config = Object.assign(new DialogConfig(), config);

    return dialogRef;
  }

  private appendDialogComponentToBody() {
    const map = new WeakMap();

    const dialogRef = new DialogRef();
    map.set(DialogRef, dialogRef);

    const sub = dialogRef.onClose.subscribe(() => {
      this.removeDialogComponentFromBody();
      sub.unsubscribe();
    });

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(DialogComponent);
    const componentRef = componentFactory.create(new DialogInjector(this.injector, map));

    this.appRef.attachView(componentRef.hostView);

    const domElem = (componentRef.hostView as EmbeddedViewRef<any>).rootNodes[0] as HTMLElement;
    document.body.appendChild(domElem);

    this.dialogComponentRef = componentRef;

    this.dialogComponentRef.instance.onClose.subscribe(() => {
      this.removeDialogComponentFromBody();
    });

    return dialogRef;
  }

  private removeDialogComponentFromBody() {
    this.appRef.detachView(this.dialogComponentRef.hostView);
    this.dialogComponentRef.destroy();
  }
}
