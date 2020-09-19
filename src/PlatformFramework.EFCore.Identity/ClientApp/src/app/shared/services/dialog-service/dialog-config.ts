export class DialogConfig<D = any> {
  width?: string = "50%";
  height?: string = "50%";
  closeOnOverlayClick?: boolean = true;
  context?: D;
}
