import { Observable, Subject } from "rxjs";

export class DialogRef {

  cancel() {
    this._onClose.next({ success: false });
  }

  close(result?: any) {
    this._onClose.next({ success: true, value: result });
  }

  private readonly _onClose = new Subject<DialogCloseResult>();
  onClose: Observable<DialogCloseResult> = this._onClose.asObservable();
}

export type DialogCloseResult = {
  success: boolean;
  value?: any;
};
