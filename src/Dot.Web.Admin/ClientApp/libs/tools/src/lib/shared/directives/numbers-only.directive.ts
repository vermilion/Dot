import { Directive, ElementRef, HostListener } from "@angular/core";

@Directive({
  selector: "input[numbersOnly]"
})
export class NumberDirective {
  constructor(private el: ElementRef) {}

  @HostListener("input", ["$event"])
  onInputChange(event: Event) {
    const initalValue = this.el.nativeElement.value;
    this.el.nativeElement.value = initalValue.replace(/[^0-9]*/g, "");
    if (initalValue !== this.el.nativeElement.value) {
      event.stopPropagation();
    }
  }
}
